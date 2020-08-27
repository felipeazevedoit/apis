﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WpNoticias.Infrastructure.Migrations
{
    public partial class NoticiasTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Noticias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Noticias");
        }
    }
}
