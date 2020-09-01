using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace wpContextEntrega.Migrations
{
    public partial class wpEntrega : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CEP",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    Bairro = table.Column<string>(nullable: true),
                    CEPFinal = table.Column<string>(nullable: true),
                    CEPInicial = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    idCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEP", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Propiedade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Altura = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Comprimento = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Largura = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Peso = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    idCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transportadora",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    idCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportadora", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Valor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    cepID = table.Column<int>(nullable: true),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    dataEdicao = table.Column<DateTime>(nullable: false),
                    idCliente = table.Column<int>(nullable: false),
                    propiedadeID = table.Column<int>(nullable: true),
                    transportadoraID = table.Column<int>(nullable: true),
                    valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Valor_CEP_cepID",
                        column: x => x.cepID,
                        principalTable: "CEP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valor_Propiedade_propiedadeID",
                        column: x => x.propiedadeID,
                        principalTable: "Propiedade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valor_Transportadora_transportadoraID",
                        column: x => x.transportadoraID,
                        principalTable: "Transportadora",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Valor_cepID",
                table: "Valor",
                column: "cepID");

            migrationBuilder.CreateIndex(
                name: "IX_Valor_propiedadeID",
                table: "Valor",
                column: "propiedadeID");

            migrationBuilder.CreateIndex(
                name: "IX_Valor_transportadoraID",
                table: "Valor",
                column: "transportadoraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Valor");

            migrationBuilder.DropTable(
                name: "CEP");

            migrationBuilder.DropTable(
                name: "Propiedade");

            migrationBuilder.DropTable(
                name: "Transportadora");
        }
    }
}
