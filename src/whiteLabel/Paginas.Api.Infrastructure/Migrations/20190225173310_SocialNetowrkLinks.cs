using Microsoft.EntityFrameworkCore.Migrations;

namespace Paginas.Api.Infrastructure.Migrations
{
    public partial class SocialNetowrkLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FabebookLink",
                table: "Paginas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IntagramLink",
                table: "Paginas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterLink",
                table: "Paginas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FabebookLink",
                table: "Paginas");

            migrationBuilder.DropColumn(
                name: "IntagramLink",
                table: "Paginas");

            migrationBuilder.DropColumn(
                name: "TwitterLink",
                table: "Paginas");
        }
    }
}
