using Microsoft.EntityFrameworkCore.Migrations;

namespace WpComunic.Infra.Migrations
{
    public partial class correcao_2_metodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_metodos_classe_classeID",
                table: "metodos");

            migrationBuilder.DropIndex(
                name: "IX_metodos_classeID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "classeID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "idMotorExterno",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "url",
                table: "metodos");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "metodos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "meio",
                table: "metodos",
                newName: "Meio");

            migrationBuilder.AddColumn<int>(
                name: "ClasseEntradaID",
                table: "metodos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClasseSaidaID",
                table: "metodos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endpoint",
                table: "metodos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_metodos_ClasseEntradaID",
                table: "metodos",
                column: "ClasseEntradaID");

            migrationBuilder.CreateIndex(
                name: "IX_metodos_ClasseSaidaID",
                table: "metodos",
                column: "ClasseSaidaID");

            migrationBuilder.AddForeignKey(
                name: "FK_metodos_classe_ClasseEntradaID",
                table: "metodos",
                column: "ClasseEntradaID",
                principalTable: "classe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_metodos_classe_ClasseSaidaID",
                table: "metodos",
                column: "ClasseSaidaID",
                principalTable: "classe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_metodos_classe_ClasseEntradaID",
                table: "metodos");

            migrationBuilder.DropForeignKey(
                name: "FK_metodos_classe_ClasseSaidaID",
                table: "metodos");

            migrationBuilder.DropIndex(
                name: "IX_metodos_ClasseEntradaID",
                table: "metodos");

            migrationBuilder.DropIndex(
                name: "IX_metodos_ClasseSaidaID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "ClasseEntradaID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "ClasseSaidaID",
                table: "metodos");

            migrationBuilder.DropColumn(
                name: "Endpoint",
                table: "metodos");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "metodos",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Meio",
                table: "metodos",
                newName: "meio");

            migrationBuilder.AddColumn<int>(
                name: "classeID",
                table: "metodos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idMotorExterno",
                table: "metodos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "metodos",
                type: "nvarchar(max)",
                nullable: true);

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
        }
    }
}
