using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class Nomeexterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeExterno",
                table: "propriedades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeExterno",
                table: "propriedades");
        }
    }
}
