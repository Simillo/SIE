using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SIE.Migrations
{
    public partial class RoomNOfStudentsMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiraionDate",
                table: "Room",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfStudents",
                table: "Room",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfStudents",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Room",
                newName: "ExpiraionDate");
        }
    }
}
