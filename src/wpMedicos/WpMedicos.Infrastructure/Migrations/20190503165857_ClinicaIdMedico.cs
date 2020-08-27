using Microsoft.EntityFrameworkCore.Migrations;

namespace WpMedicos.Infrastructure.Migrations
{
    public partial class ClinicaIdMedico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telefones_MedicoId",
                table: "Telefones");

            migrationBuilder.AddColumn<int>(
                name: "ClinicaId",
                table: "Medicos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_MedicoId",
                table: "Telefones",
                column: "MedicoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telefones_MedicoId",
                table: "Telefones");

            migrationBuilder.DropColumn(
                name: "ClinicaId",
                table: "Medicos");

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_MedicoId",
                table: "Telefones",
                column: "MedicoId");
        }
    }
}
