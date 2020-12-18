using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class correcao_metodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "metodos");

            migrationBuilder.AddColumn<string>(
                name: "meio",
                table: "metodos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "meio",
                table: "metodos");

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "metodos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
