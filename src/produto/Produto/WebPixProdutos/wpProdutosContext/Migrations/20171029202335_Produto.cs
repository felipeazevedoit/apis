using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Repository.Migrations
{
    public partial class Produto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preco",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecoPromocional = table.Column<int>(type: "int", nullable: false),
                    IDProduto = table.Column<int>(type: "int", nullable: false),
                    PrecoReal = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioEdicao = table.Column<int>(type: "int", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataEdicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preco", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Catalogo = table.Column<int>(type: "int", nullable: false),
                    CodExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comprimento = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoLonga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estoque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstoqueMinimo = table.Column<int>(type: "int", nullable: false),
                    Fabrinte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Largura = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    PrecoID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioEdicao = table.Column<int>(type: "int", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataEdicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produto_Preco_PrecoID",
                        column: x => x.PrecoID,
                        principalTable: "Preco",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoSku",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CodSkuExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDProduto = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdutoID = table.Column<int>(type: "int", nullable: true),
                    SkuEstoque = table.Column<int>(type: "int", nullable: false),
                    SkuPeso = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioEdicao = table.Column<int>(type: "int", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataEdicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoSku", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProdutoSku_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Propiedades",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdutoSkuID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioEdicao = table.Column<int>(type: "int", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataEdicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Propiedades_ProdutoSku_ProdutoSkuID",
                        column: x => x.ProdutoSkuID,
                        principalTable: "ProdutoSku",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_PrecoID",
                table: "Produto",
                column: "PrecoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoSku_ProdutoID",
                table: "ProdutoSku",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_ProdutoSkuID",
                table: "Propiedades",
                column: "ProdutoSkuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedades");

            migrationBuilder.DropTable(
                name: "ProdutoSku");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Preco");
        }
    }
}
