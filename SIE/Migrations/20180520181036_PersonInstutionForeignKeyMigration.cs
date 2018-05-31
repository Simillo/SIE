using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SIE.Migrations
{
    public partial class PersonInstutionForeignKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Person_InstitutionId",
                table: "Person",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Institution_InstitutionId",
                table: "Person",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Institution_InstitutionId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_InstitutionId",
                table: "Person");
        }
    }
}
