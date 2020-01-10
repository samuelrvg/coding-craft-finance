using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Finance.Attributes;
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

        public System.Data.Entity.DbSet<Finance.Models.Banco> Bancos { get; set; }

        public System.Data.Entity.DbSet<Finance.Models.ReceitaCategoria> ReceitaCategorias { get; set; }

        public System.Data.Entity.DbSet<Finance.Models.DespesaCategoria> DespesaCategorias { get; set; }
    }
}