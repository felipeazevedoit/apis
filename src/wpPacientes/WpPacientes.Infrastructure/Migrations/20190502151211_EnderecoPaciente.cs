using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpPacientes.Infrastructure.Migrations
{
    public partial class EnderecoPaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataEdicao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CEP = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Local = table.Column<string>(nullable: true),
                    NumeroLocal = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    IdUsuario = table.Column<int>(nullable: false),
                    Uf = table.Column<string>(nullable: true),
                    PacienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enderecos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_PacienteId",
                table: "Enderecos",
                column: "PacienteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
