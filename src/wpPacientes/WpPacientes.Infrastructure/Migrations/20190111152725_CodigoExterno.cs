using Microsoft.EntityFrameworkCore.Migrations;

namespace WpPacientes.Infrastructure.Migrations
{
    public partial class CodigoExterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoExterno",
                table: "Pacientes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoExterno",
                table: "Pacientes");
        }
    }
}
