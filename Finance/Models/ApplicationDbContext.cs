using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Finance.Attributes;
using Finance.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Finance.Models
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add<CascadeDeleteAttributeConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<ReceitaCategoria> ReceitaCategorias { get; set; }
        public DbSet<DespesaCategoria> DespesaCategorias { get; set; }

        public override int SaveChanges()
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
                    }
                }

                return base.SaveChanges();
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