using Microsoft.EntityFrameworkCore.Migrations;

namespace WpPacientes.Infrastructure.Migrations
{
    public partial class PacienteConvenio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConvenioId",
                table: "Pacientes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvenioId",
                table: "Pacientes");
        }
    }
}
