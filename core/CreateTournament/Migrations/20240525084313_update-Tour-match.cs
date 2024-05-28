using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreateTournament.Migrations
{
    public partial class updateTourmatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "round",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "round",
                table: "Matches");
        }
    }
}
