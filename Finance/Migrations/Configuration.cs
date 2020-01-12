namespace Finance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Finance.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Finance.Models.ApplicationDbContext context)
        {
            //context.DespesaCategorias.AddOrUpdate(
            //    new Models.DespesaCategoria() { Nome = "Alimentação"},
            //    new Models.DespesaCategoria() { Nome = "Casa" }
            //    );

            //context.ReceitaCategorias.AddOrUpdate(
            //    new Models.ReceitaCategoria() { Nome = "Salário" },
            //    new Models.ReceitaCategoria() { Nome = "Outros" }
            //    );

            //context.Bancos.AddOrUpdate(
            //    new Models.Banco() { Nome = "Bradesco" },
            //    new Models.Banco() { Nome = "Nubank" }
            //    );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
