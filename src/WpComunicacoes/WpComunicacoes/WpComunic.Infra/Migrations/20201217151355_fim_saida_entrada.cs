using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class fim_saida_entrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_metodos_entradas_entradaID",
                table: "metodos");

            migrationBuilder.DropForeignKey(
                name: "FK_metodos_saidas_saidaID",
                table: "metodos");

            migrationBuilder.DropForeignKey(
                name: "FK_motorExternos_funcoes_funcaoID",
                table: "motorExternos");

            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_entradas_EntradaID",
                table: "propriedades");

            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_saidas_SaidaID",
                table: "propriedades");

            migrationBuilder.DropTable(
                name: "entradas");

            migrationBuilder.DropTable(
                name: "saidas");

            migrationBuilder.DropIndex(
                name: "IX_propriedades_EntradaID",
                table: "propriedades");

            migrationBuilder.DropIndex(
                name: "IX_propriedades_SaidaID",
                table: "propriedades");

            migrationBuilder.DropIndex(
                name: "IX_metodos_entradaID",
                table: "metodos");

            migrationBuilder.DropIndex(
                name: "IX_metodos_saidaID",
                table: "metodos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_funcoes",
                table: "funcoes");

            migrationBuilder.DropColumn(
                name: "EntradaID",
                table: "propriedades");

            migrationBuilder.DropColumn(
                name: "SaidaID",
                table: "propriedades");

            migrationBuilder.DropColumn(
                name: "entradaID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "saidaID",
                table: "metodos");

            migrationBuilder.RenameTable(
                name: "funcoes",
                newName: "Funcao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcao",
                table: "Funcao",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_motorExternos_Funcao_funcaoID",
                table: "motorExternos",
                column: "funcaoID",
                principalTable: "Funcao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_motorExternos_Funcao_funcaoID",
                table: "motorExternos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcao",
                table: "Funcao");

            migrationBuilder.RenameTable(
                name: "Funcao",
                newName: "funcoes");

            migrationBuilder.AddColumn<int>(
                name: "EntradaID",
                table: "propriedades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaidaID",
                table: "propriedades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "entradaID",
                table: "metodos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "saidaID",
                table: "metodos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcoes",
                table: "funcoes",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "entradas",
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
                    table.PrimaryKey("PK_entradas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "saidas",
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
                    table.PrimaryKey("PK_saidas", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_propriedades_EntradaID",
                table: "propriedades",
                column: "EntradaID");

            migrationBuilder.CreateIndex(
                name: "IX_propriedades_SaidaID",
                table: "propriedades",
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
                name: "FK_motorExternos_funcoes_funcaoID",
                table: "motorExternos",
                column: "funcaoID",
                principalTable: "funcoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
        }
    }
}
