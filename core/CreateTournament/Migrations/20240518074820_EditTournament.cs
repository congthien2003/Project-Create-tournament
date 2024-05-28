using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreateTournament.Migrations
{
    public partial class EditTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TouramentId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "TouramentId",
                table: "Matches",
                newName: "TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_TouramentId",
                table: "Matches",
                newName: "IX_Matches_TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "Matches",
                newName: "TouramentId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                newName: "IX_Matches_TouramentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TouramentId",
                table: "Matches",
                column: "TouramentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
