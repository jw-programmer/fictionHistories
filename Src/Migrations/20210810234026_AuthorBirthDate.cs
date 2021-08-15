using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Src.Migrations
{
    public partial class AuthorBirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Authors");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Authors",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
