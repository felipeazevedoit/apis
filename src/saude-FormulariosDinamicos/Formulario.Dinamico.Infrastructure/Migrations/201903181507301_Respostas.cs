namespace Formulario.Dinamico.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Respostas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perguntas", "TipoRespostaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Perguntas", "TipoRespostaId");
            AddForeignKey("dbo.Perguntas", "TipoRespostaId", "dbo.TiposResposta", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Perguntas", "TipoRespostaId", "dbo.TiposResposta");
            DropIndex("dbo.Perguntas", new[] { "TipoRespostaId" });
            DropColumn("dbo.Perguntas", "TipoRespostaId");
        }
    }
}
