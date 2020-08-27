using Microsoft.EntityFrameworkCore.Migrations;

namespace WpNoticias.Infrastructure.Migrations
{
    public partial class Tipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Noticias",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Noticias");
        }
    }
}
