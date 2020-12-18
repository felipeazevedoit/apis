using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class atualizzação_tradutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tradutorID",
                table: "motorExternos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tradutor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DateAlteracao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    idCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tradutor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Propriedades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DateAlteracao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    idCliente = table.Column<int>(nullable: false),
                    tipo = table.Column<string>(nullable: true),
                    TradutorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propriedades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Propriedades_Tradutor_TradutorID",
                        column: x => x.TradutorID,
                        principalTable: "Tradutor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_motorExternos_tradutorID",
                table: "motorExternos",
                column: "tradutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Propriedades_TradutorID",
                table: "Propriedades",
                column: "TradutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_motorExternos_Tradutor_tradutorID",
                table: "motorExternos",
                column: "tradutorID",
                principalTable: "Tradutor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_motorExternos_Tradutor_tradutorID",
                table: "motorExternos");

            migrationBuilder.DropTable(
                name: "Propriedades");

            migrationBuilder.DropTable(
                name: "Tradutor");

            migrationBuilder.DropIndex(
                name: "IX_motorExternos_tradutorID",
                table: "motorExternos");

            migrationBuilder.DropColumn(
                name: "tradutorID",
                table: "motorExternos");
        }
    }
}
