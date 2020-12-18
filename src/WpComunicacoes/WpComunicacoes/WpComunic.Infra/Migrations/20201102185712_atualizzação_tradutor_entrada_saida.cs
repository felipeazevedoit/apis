using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class atualizzação_tradutor_entrada_saida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_motorExternos_Tradutor_tradutorID",
                table: "motorExternos");

            migrationBuilder.DropForeignKey(
                name: "FK_Propriedades_Tradutor_TradutorID",
                table: "Propriedades");

            migrationBuilder.DropTable(
                name: "Tradutor");

            migrationBuilder.DropIndex(
                name: "IX_Propriedades_TradutorID",
                table: "Propriedades");

            migrationBuilder.DropIndex(
                name: "IX_motorExternos_tradutorID",
                table: "motorExternos");

            migrationBuilder.DropColumn(
                name: "modelo",
                table: "saidas");

            migrationBuilder.DropColumn(
                name: "TradutorID",
                table: "Propriedades");

            migrationBuilder.DropColumn(
                name: "modelo",
                table: "motorExternos");

            migrationBuilder.DropColumn(
                name: "tradutorID",
                table: "motorExternos");

            migrationBuilder.AddColumn<int>(
                name: "EntradaID",
                table: "Propriedades",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaidaID",
                table: "Propriedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "valor",
                table: "Propriedades",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "entradaID",
                table: "metodos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "saidaID",
                table: "metodos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "entradas",
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
                    table.PrimaryKey("PK_entradas", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propriedades_EntradaID",
                table: "Propriedades",
                column: "EntradaID");

            migrationBuilder.CreateIndex(
                name: "IX_Propriedades_SaidaID",
                table: "Propriedades",
                column: "SaidaID");

            migrationBuilder.CreateIndex(
                name: "IX_metodos_entradaID",
                table: "metodos",
                column: "entradaID");

            migrationBuilder.CreateIndex(
                name: "IX_metodos_saidaID",
                table: "metodos",
                column: "saidaID");

            migrationBuilder.AddForeignKey(
                name: "FK_metodos_entradas_entradaID",
                table: "metodos",
                column: "entradaID",
                principalTable: "entradas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_metodos_saidas_saidaID",
                table: "metodos",
                column: "saidaID",
                principalTable: "saidas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Propriedades_entradas_EntradaID",
                table: "Propriedades",
                column: "EntradaID",
                principalTable: "entradas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Propriedades_saidas_SaidaID",
                table: "Propriedades",
                column: "SaidaID",
                principalTable: "saidas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_metodos_entradas_entradaID",
                table: "metodos");

            migrationBuilder.DropForeignKey(
                name: "FK_metodos_saidas_saidaID",
                table: "metodos");

            migrationBuilder.DropForeignKey(
                name: "FK_Propriedades_entradas_EntradaID",
                table: "Propriedades");

            migrationBuilder.DropForeignKey(
                name: "FK_Propriedades_saidas_SaidaID",
                table: "Propriedades");

            migrationBuilder.DropTable(
                name: "entradas");

            migrationBuilder.DropIndex(
                name: "IX_Propriedades_EntradaID",
                table: "Propriedades");

            migrationBuilder.DropIndex(
                name: "IX_Propriedades_SaidaID",
                table: "Propriedades");

            migrationBuilder.DropIndex(
                name: "IX_metodos_entradaID",
                table: "metodos");

            migrationBuilder.DropIndex(
                name: "IX_metodos_saidaID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "EntradaID",
                table: "Propriedades");

            migrationBuilder.DropColumn(
                name: "SaidaID",
                table: "Propriedades");

            migrationBuilder.DropColumn(
                name: "valor",
                table: "Propriedades");

            migrationBuilder.DropColumn(
                name: "entradaID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "saidaID",
                table: "metodos");

            migrationBuilder.AddColumn<string>(
                name: "modelo",
                table: "saidas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TradutorID",
                table: "Propriedades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modelo",
                table: "motorExternos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tradutorID",
                table: "motorExternos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tradutor",
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
                    idCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tradutor", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propriedades_TradutorID",
                table: "Propriedades",
                column: "TradutorID");

            migrationBuilder.CreateIndex(
                name: "IX_motorExternos_tradutorID",
                table: "motorExternos",
                column: "tradutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_motorExternos_Tradutor_tradutorID",
                table: "motorExternos",
                column: "tradutorID",
                principalTable: "Tradutor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Propriedades_Tradutor_TradutorID",
                table: "Propriedades",
                column: "TradutorID",
                principalTable: "Tradutor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
