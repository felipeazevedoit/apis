namespace Formulario.Dinamico.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perguntas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerguntaPaiId = c.Int(nullable: false),
                        Nome = c.String(),
                        Descricao = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DateAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioEdicao = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perguntas", t => t.PerguntaPaiId)
                .Index(t => t.PerguntaPaiId);
            
            CreateTable(
                "dbo.Respostas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodigoExterno = c.Int(nullable: false),
                        PerguntaId = c.Int(nullable: false),
                        Nome = c.String(),
                        Descricao = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DateAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioEdicao = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perguntas", t => t.PerguntaId, cascadeDelete: true)
                .Index(t => t.PerguntaId);
            
            CreateTable(
                "dbo.TiposResposta",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DateAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioEdicao = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respostas", "PerguntaId", "dbo.Perguntas");
            DropForeignKey("dbo.Perguntas", "PerguntaPaiId", "dbo.Perguntas");
            DropIndex("dbo.Respostas", new[] { "PerguntaId" });
            DropIndex("dbo.Perguntas", new[] { "PerguntaPaiId" });
            DropTable("dbo.TiposResposta");
            DropTable("dbo.Respostas");
            DropTable("dbo.Perguntas");
        }
    }
}
