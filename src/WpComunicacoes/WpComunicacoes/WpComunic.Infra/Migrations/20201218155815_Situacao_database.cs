using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class Situacao_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_tradutor_TradutorID",
                table: "propriedades");

            migrationBuilder.DropTable(
                name: "tradutor");

            migrationBuilder.DropIndex(
                name: "IX_propriedades_TradutorID",
                table: "propriedades");

            migrationBuilder.DropColumn(
                name: "TradutorID",
                table: "propriedades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TradutorID",
                table: "propriedades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tradutor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioEdicao = table.Column<int>(type: "int", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    metodoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tradutor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tradutor_metodos_metodoID",
                        column: x => x.metodoID,
                        principalTable: "metodos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_propriedades_TradutorID",
                table: "propriedades",
                column: "TradutorID");

            migrationBuilder.CreateIndex(
                name: "IX_tradutor_metodoID",
                table: "tradutor",
                column: "metodoID");

            migrationBuilder.AddForeignKey(
                name: "FK_propriedades_tradutor_TradutorID",
                table: "propriedades",
                column: "TradutorID",
                principalTable: "tradutor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
