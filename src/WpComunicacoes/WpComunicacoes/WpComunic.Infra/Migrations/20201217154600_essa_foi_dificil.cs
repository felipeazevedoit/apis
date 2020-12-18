using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class essa_foi_dificil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_classe_classeID",
                table: "propriedades");

            migrationBuilder.DropColumn(
                name: "idPropriedades",
                table: "classe");

            migrationBuilder.RenameColumn(
                name: "classeID",
                table: "propriedades",
                newName: "ClasseID");

            migrationBuilder.RenameIndex(
                name: "IX_propriedades_classeID",
                table: "propriedades",
                newName: "IX_propriedades_ClasseID");

            migrationBuilder.AddForeignKey(
                name: "FK_propriedades_classe_ClasseID",
                table: "propriedades",
                column: "ClasseID",
                principalTable: "classe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_propriedades_classe_ClasseID",
                table: "propriedades");

            migrationBuilder.RenameColumn(
                name: "ClasseID",
                table: "propriedades",
                newName: "classeID");

            migrationBuilder.RenameIndex(
                name: "IX_propriedades_ClasseID",
                table: "propriedades",
                newName: "IX_propriedades_classeID");

            migrationBuilder.AddColumn<string>(
                name: "idPropriedades",
                table: "classe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_propriedades_classe_classeID",
                table: "propriedades",
                column: "classeID",
                principalTable: "classe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
