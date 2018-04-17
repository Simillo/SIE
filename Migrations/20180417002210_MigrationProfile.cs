using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SIE.Migrations
{
    public partial class MigrationProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBirth",
                table: "Person",
                newName: "BirthDate");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Person",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Profile",
                table: "Person",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Person",
                newName: "DateBirth");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Person",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
