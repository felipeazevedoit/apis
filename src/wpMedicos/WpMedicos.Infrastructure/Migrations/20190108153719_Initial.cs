using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpMedicos.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(250)", nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataEdicao = table.Column<DateTime>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false),
                    UsuarioEdicao = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CodigoExterno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicos");
        }
    }
}
