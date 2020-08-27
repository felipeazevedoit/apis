using Microsoft.EntityFrameworkCore.Migrations;

namespace WpMedicos.Infrastructure.Migrations
{
    public partial class Clinica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicaNome",
                table: "Medicos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicaNome",
                table: "Medicos");
        }
    }
}
