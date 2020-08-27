using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpMedicos.Infrastructure.Migrations
{
    public partial class MedicoPropriedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Medicos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CRM",
                table: "Medicos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Medicos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "CRM",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Medicos");
        }
    }
}
