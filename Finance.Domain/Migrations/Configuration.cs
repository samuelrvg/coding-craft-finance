namespace Finance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Finance.Models.FinanceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.FinanceContext context)
        {
            var despesaCategoria1 = new Models.DespesaCategoria() { Nome = "Alimentacao" };
            var despesaCategoria2 = new Models.DespesaCategoria() { Nome = "Casa" };

            context.DespesaCategorias.AddOrUpdate(dc => dc.Nome, despesaCategoria1);
            context.DespesaCategorias.AddOrUpdate(dc => dc.Nome, despesaCategoria2);

            var receitaCategoria1 = new Models.ReceitaCategoria() { Nome = "Salario" };
            var receitaCategoria2 = new Models.ReceitaCategoria() { Nome = "Outros" };

            context.ReceitaCategorias.AddOrUpdate(rc => rc.Nome, receitaCategoria1);
            context.ReceitaCategorias.AddOrUpdate(rc => rc.Nome, receitaCategoria2);

            var banco1 = new Models.Banco() { Nome = "Bradesco", DataCriacao = DateTime.Now, UsuarioCriacao = "Samuel" };
            var banco2 = new Models.Banco() { Nome = "Nubank", DataCriacao = DateTime.Now, UsuarioCriacao = "Renan" };
            var banco3 = new Models.Banco() { Nome = "Caixa", DataCriacao = DateTime.Now, UsuarioCriacao = "Davi" };

            context.Bancos.AddOrUpdate(b => b.Nome, banco1);
            context.Bancos.AddOrUpdate(b => b.Nome, banco2);
            context.Bancos.AddOrUpdate(b => b.Nome, banco3);

            var depasa1 = new Models.Despesa() { Descricao = "Alimentacao", Valor = 512, DespesaCategoria = despesaCategoria1 };
            var depasa2 = new Models.Despesa() { Descricao = "Uber", Valor = 655, DespesaCategoria = despesaCategoria2 };
            var depasa3 = new Models.Despesa() { Descricao = "Uber Eats", Valor = 996, DespesaCategoria = despesaCategoria1 };

            context.Despesas.AddOrUpdate(d => d.Descricao, depasa1);
            context.Despesas.AddOrUpdate(d => d.Descricao, depasa2);
            context.Despesas.AddOrUpdate(d => d.Descricao, depasa3);

            var conta1 = new Models.Conta() { Banco = banco1, Descricao = "Conta 1", Nome = "Pessoal", SaldoAtual = 9556 };
            var conta2 = new Models.Conta() { Banco = banco2, Descricao = "Conta 2", Nome = "PJ", SaldoAtual = 1233 };

            context.Contas.AddOrUpdate(d => d.Nome, conta1);
            context.Contas.AddOrUpdate(d => d.Nome, conta2);

            var receita1 = new Models.Receita() { Descricao = "Salario", Valor = 432, Banco = banco1, ReceitaCategoria = receitaCategoria1 };
            var receita2 = new Models.Receita() { Descricao = "Outros", Valor = 988, Banco = banco2, ReceitaCategoria = receitaCategoria2 };

            context.Receitas.AddOrUpdate(r => r.Descricao, receita1);
            context.Receitas.AddOrUpdate(r => r.Descricao, receita2);

            var transferencia1 = new Models.Transferencia() { Valor = 512, ContaOrigem = conta1, ContaDestino = conta2 };
            var transferencia2 = new Models.Transferencia() { Valor = 13, ContaOrigem = conta2, ContaDestino = conta1 };

            context.Transferencias.AddOrUpdate(t => t.TransferenciaId, transferencia1);
            context.Transferencias.AddOrUpdate(t => t.TransferenciaId, transferencia2);
        }
    }
}
