using Microsoft.EntityFrameworkCore.Migrations;

namespace WpNoticias.Infrastructure.Migrations
{
    public partial class NoticiaCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Noticias",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_CategoriaId",
                table: "Noticias",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias");

            migrationBuilder.DropIndex(
                name: "IX_Noticias_CategoriaId",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Noticias");
        }
    }
}
