using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreateTournament.Migrations
{
    public partial class updateassistscolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Assits",
                table: "PlayerStats",
                newName: "Assists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Assists",
                table: "PlayerStats",
                newName: "Assits");
        }
    }
}
