using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Paginas.Api.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paginas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DateAlteracao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false),
                    CodigoExterno = table.Column<int>(nullable: false),
                    Banner = table.Column<byte[]>(nullable: true),
                    Apresentacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paginas", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paginas");
        }
    }
}
