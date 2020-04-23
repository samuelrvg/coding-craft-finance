using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Finance.Attributes;
using Finance.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Finance.Models
{
    public class FinanceContext : IdentityDbContext<Usuario>
    {
        public FinanceContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FinanceContext Create()
        {
            return new FinanceContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add<CascadeDeleteAttributeConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<DespesaCategoria> DespesaCategorias { get; set; }
        public DbSet<ReceitaCategoria> ReceitaCategorias { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<BancoAuditoria> BancoAuditorias { get; set; }

        public override Task<int> SaveChangesAsync()
        {
            Auditoria();
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            Auditoria();
            return base.SaveChanges();
        }

        private void Auditoria()
        {
            try
            {
                var currentTime = DateTime.Now;

                foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity != null &&
                        typeof(IEntidadeNaoEditavel).IsAssignableFrom(e.Entity.GetType())))
                {
                    if (entry.State == EntityState.Added)
                    {

                        if (entry.Property("DataCriacao") != null)
                        {
                            entry.Property("DataCriacao").CurrentValue = currentTime;
                        }
                        if (entry.Property("UsuarioCriacao") != null)
                        {
                            entry.Property("UsuarioCriacao").CurrentValue = HttpContext.Current != null ? HttpContext.Current.User.Identity.Name : "Usuario";
                        }
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCriacao").IsModified = false;
                        entry.Property("UsuarioCriacao").IsModified = false;

                        if (entry.Property("UltimaModificacao") != null)
                        {
                            entry.Property("UltimaModificacao").CurrentValue = currentTime;
                        }
                        if (entry.Property("UsuarioModificacao") != null)
                        {
                            entry.Property("UsuarioModificacao").CurrentValue = HttpContext.Current != null ? HttpContext.Current.User.Identity.Name : "Usuario";
                        }

                        foreach (var entidade in ChangeTracker.Entries())
                        {
                            var tipoTabelaAuditoria = entidade.Entity.GetType().GetInterfaces()[0].GenericTypeArguments[0];
                            var registroTabelaAuditoria = Activator.CreateInstance(tipoTabelaAuditoria);

                            foreach (var propriedade in entidade.Entity.GetType().GetProperties())
                            {
                                if (propriedade.Name == "UsuarioModificacao" || propriedade.Name == "UltimaModificacao")
                                    continue;

                                registroTabelaAuditoria.GetType()
                                                       .GetProperty(propriedade.Name)
                                                       .SetValue(registroTabelaAuditoria, entidade.Entity.GetType().GetProperty(propriedade.Name).GetValue(entidade.Entity, null));
                            }

                            this.Set(registroTabelaAuditoria.GetType()).Add(registroTabelaAuditoria);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }
        }
    }
}