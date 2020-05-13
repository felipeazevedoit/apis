namespace Mensagens.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensagens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RemetenteId = c.Int(nullable: false),
                        DestinatarioId = c.Int(nullable: false),
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
            DropTable("dbo.Mensagens");
        }
    }
}
