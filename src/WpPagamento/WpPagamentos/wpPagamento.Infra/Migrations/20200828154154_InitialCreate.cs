using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wpPagamento.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meioPagamentos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    idCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meioPagamentos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Propiedade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    idCliente = table.Column<int>(nullable: false),
                    Moeda = table.Column<int>(nullable: false),
                    Recalculo = table.Column<bool>(nullable: false),
                    Meio = table.Column<bool>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Parcela = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "loja",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    idCliente = table.Column<int>(nullable: false),
                    idPedido = table.Column<int>(nullable: false),
                    propiedadesID = table.Column<int>(nullable: true),
                    meioPagamentoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_loja_meioPagamentos_meioPagamentoID",
                        column: x => x.meioPagamentoID,
                        principalTable: "meioPagamentos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_loja_Propiedade_propiedadesID",
                        column: x => x.propiedadesID,
                        principalTable: "Propiedade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_loja_meioPagamentoID",
                table: "loja",
                column: "meioPagamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_loja_propiedadesID",
                table: "loja",
                column: "propiedadesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loja");

            migrationBuilder.DropTable(
                name: "meioPagamentos");

            migrationBuilder.DropTable(
                name: "Propiedade");
        }
    }
}
