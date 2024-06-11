using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreateTournament.Migrations
{
    public partial class updatemodeltournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishAt",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishAt",
                table: "Tournaments");
        }
    }
}
