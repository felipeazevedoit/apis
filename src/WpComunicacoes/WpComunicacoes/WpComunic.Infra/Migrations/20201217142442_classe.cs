using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class classe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "classeID",
                table: "propriedades",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "classeID",
                table: "metodos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "classe",
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
                    idPropriedades = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classe", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_propriedades_classeID",
                table: "propriedades",
                column: "classeID");

            migrationBuilder.CreateIndex(
                name: "IX_metodos_classeID",
                table: "metodos",
                column: "classeID");

            migrationBuilder.AddForeignKey(
                name: "FK_metodos_classe_classeID",
                table: "metodos",
                column: "classeID",
                principalTable: "classe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_propriedades_classe_classeID",
                table: "propriedades",
                column: "classeID",
                principalTable: "classe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_metodos_classe_classeID",
                table: "metodos");

            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_classe_classeID",
                table: "propriedades");

            migrationBuilder.DropTable(
                name: "classe");

            migrationBuilder.DropIndex(
                name: "IX_propriedades_classeID",
                table: "propriedades");

            migrationBuilder.DropIndex(
                name: "IX_metodos_classeID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "classeID",
                table: "propriedades");

            migrationBuilder.DropColumn(
                name: "classeID",
                table: "metodos");
        }
    }
}
