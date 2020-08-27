namespace Formulario.Dinamico.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Perguntas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Perguntas", "PerguntaPaiId", "dbo.Perguntas");
            DropIndex("dbo.Perguntas", new[] { "PerguntaPaiId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Perguntas", "PerguntaPaiId");
            AddForeignKey("dbo.Perguntas", "PerguntaPaiId", "dbo.Perguntas", "ID");
        }
    }
}
