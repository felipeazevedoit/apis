namespace Formulario.Dinamico.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crptografias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Criptografias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodigoExterno = c.Int(nullable: false),
                        ChaveExterna = c.Binary(),
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
            DropTable("dbo.Criptografias");
        }
    }
}
