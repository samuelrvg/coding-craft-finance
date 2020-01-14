namespace Finance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BancoAuditoria",
                c => new
                    {
                        BancoAuditoriaId = c.Int(nullable: false, identity: true),
                        BancoId = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 200),
                        DataCriacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioCriacao = c.String(),
                    })
                .PrimaryKey(t => t.BancoAuditoriaId)
                .Index(t => t.Nome, name: "IUQ_BancosAuditoria_Nome");
            
            CreateTable(
                "dbo.Contas",
                c => new
                    {
                        ContaId = c.Int(nullable: false, identity: true),
                        BancoId = c.Int(nullable: false),
                        UsuarioId = c.String(maxLength: 128),
                        Nome = c.String(nullable: false, maxLength: 300),
                        Descricao = c.String(),
                        SaldoAtual = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BancoAuditoria_BancoAuditoriaId = c.Int(),
                    })
                .PrimaryKey(t => t.ContaId)
                .ForeignKey("dbo.Bancos", t => t.BancoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .ForeignKey("dbo.BancoAuditoria", t => t.BancoAuditoria_BancoAuditoriaId)
                .Index(t => t.BancoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.BancoAuditoria_BancoAuditoriaId);
            
            CreateTable(
                "dbo.Bancos",
                c => new
                    {
                        BancoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200),
                        UltimaModificacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioModificacao = c.String(maxLength: 200),
                        DataCriacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioCriacao = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.BancoId)
                .Index(t => t.Nome, name: "IUQ_Bancos_Nome");
            
            CreateTable(
                "dbo.Receitas",
                c => new
                    {
                        ReceitaId = c.Int(nullable: false, identity: true),
                        ReceitaCategoriaId = c.Int(nullable: false),
                        BancoId = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 1024),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Conta_ContaId = c.Int(),
                    })
                .PrimaryKey(t => t.ReceitaId)
                .ForeignKey("dbo.Bancos", t => t.BancoId)
                .ForeignKey("dbo.ReceitaCategorias", t => t.ReceitaCategoriaId)
                .ForeignKey("dbo.Contas", t => t.Conta_ContaId)
                .Index(t => t.ReceitaCategoriaId)
                .Index(t => t.BancoId)
                .Index(t => t.Conta_ContaId);
            
            CreateTable(
                "dbo.ReceitaCategorias",
                c => new
                    {
                        ReceitaCategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.ReceitaCategoriaId)
                .Index(t => t.Nome, unique: true, name: "IUQ_ReceitaCategoria_Nome");
            
            CreateTable(
                "dbo.Transferencias",
                c => new
                    {
                        TransferenciaId = c.Int(nullable: false, identity: true),
                        ContaOrigemId = c.Int(nullable: false),
                        ContaDestinoId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransferenciaId)
                .ForeignKey("dbo.Contas", t => t.ContaDestinoId)
                .ForeignKey("dbo.Contas", t => t.ContaOrigemId)
                .Index(t => t.ContaOrigemId)
                .Index(t => t.ContaDestinoId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.DespesaCategorias",
                c => new
                    {
                        DespesaCategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.DespesaCategoriaId)
                .Index(t => t.Nome, unique: true, name: "IUQ_DespesaCategoria_Nome");
            
            CreateTable(
                "dbo.Despesas",
                c => new
                    {
                        DespesaId = c.Int(nullable: false, identity: true),
                        DespesaCategoriaId = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 1024),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DespesaId)
                .ForeignKey("dbo.DespesaCategorias", t => t.DespesaCategoriaId)
                .Index(t => t.DespesaCategoriaId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Despesas", "DespesaCategoriaId", "dbo.DespesaCategorias");
            DropForeignKey("dbo.Contas", "BancoAuditoria_BancoAuditoriaId", "dbo.BancoAuditoria");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contas", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transferencias", "ContaOrigemId", "dbo.Contas");
            DropForeignKey("dbo.Transferencias", "ContaDestinoId", "dbo.Contas");
            DropForeignKey("dbo.Receitas", "Conta_ContaId", "dbo.Contas");
            DropForeignKey("dbo.Receitas", "ReceitaCategoriaId", "dbo.ReceitaCategorias");
            DropForeignKey("dbo.Receitas", "BancoId", "dbo.Bancos");
            DropForeignKey("dbo.Contas", "BancoId", "dbo.Bancos");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Despesas", new[] { "DespesaCategoriaId" });
            DropIndex("dbo.DespesaCategorias", "IUQ_DespesaCategoria_Nome");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Transferencias", new[] { "ContaDestinoId" });
            DropIndex("dbo.Transferencias", new[] { "ContaOrigemId" });
            DropIndex("dbo.ReceitaCategorias", "IUQ_ReceitaCategoria_Nome");
            DropIndex("dbo.Receitas", new[] { "Conta_ContaId" });
            DropIndex("dbo.Receitas", new[] { "BancoId" });
            DropIndex("dbo.Receitas", new[] { "ReceitaCategoriaId" });
            DropIndex("dbo.Bancos", "IUQ_Bancos_Nome");
            DropIndex("dbo.Contas", new[] { "BancoAuditoria_BancoAuditoriaId" });
            DropIndex("dbo.Contas", new[] { "UsuarioId" });
            DropIndex("dbo.Contas", new[] { "BancoId" });
            DropIndex("dbo.BancoAuditoria", "IUQ_BancosAuditoria_Nome");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Despesas");
            DropTable("dbo.DespesaCategorias");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Transferencias");
            DropTable("dbo.ReceitaCategorias");
            DropTable("dbo.Receitas");
            DropTable("dbo.Bancos");
            DropTable("dbo.Contas");
            DropTable("dbo.BancoAuditoria");
        }
    }
}
