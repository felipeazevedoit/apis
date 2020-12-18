using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class atualizzação_tradutor_entrada_saida_tradutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propriedades_entradas_EntradaID",
                table: "Propriedades");

            migrationBuilder.DropForeignKey(
                name: "FK_Propriedades_saidas_SaidaID",
                table: "Propriedades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propriedades",
                table: "Propriedades");

            migrationBuilder.RenameTable(
                name: "Propriedades",
                newName: "propriedades");

            migrationBuilder.RenameIndex(
                name: "IX_Propriedades_SaidaID",
                table: "propriedades",
                newName: "IX_propriedades_SaidaID");

            migrationBuilder.RenameIndex(
                name: "IX_Propriedades_EntradaID",
                table: "propriedades",
                newName: "IX_propriedades_EntradaID");

            migrationBuilder.AddColumn<int>(
                name: "TradutorID",
                table: "propriedades",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_propriedades",
                table: "propriedades",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "tradutor",
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
                    metodoID = table.Column<int>(nullable: true)
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
                name: "FK_propriedades_entradas_EntradaID",
                table: "propriedades",
                column: "EntradaID",
                principalTable: "entradas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_propriedades_saidas_SaidaID",
                table: "propriedades",
                column: "SaidaID",
                principalTable: "saidas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_propriedades_tradutor_TradutorID",
                table: "propriedades",
                column: "TradutorID",
                principalTable: "tradutor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_entradas_EntradaID",
                table: "propriedades");

            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_saidas_SaidaID",
                table: "propriedades");

            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_tradutor_TradutorID",
                table: "propriedades");

            migrationBuilder.DropTable(
                name: "tradutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_propriedades",
                table: "propriedades");

            migrationBuilder.DropIndex(
                name: "IX_propriedades_TradutorID",
                table: "propriedades");

            migrationBuilder.DropColumn(
                name: "TradutorID",
                table: "propriedades");

            migrationBuilder.RenameTable(
                name: "propriedades",
                newName: "Propriedades");

            migrationBuilder.RenameIndex(
                name: "IX_propriedades_SaidaID",
                table: "Propriedades",
                newName: "IX_Propriedades_SaidaID");

            migrationBuilder.RenameIndex(
                name: "IX_propriedades_EntradaID",
                table: "Propriedades",
                newName: "IX_Propriedades_EntradaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propriedades",
                table: "Propriedades",
                column: "ID");

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
    }
}
