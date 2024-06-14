using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreateTournament.Migrations
{
    public partial class InitTypeOfMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<bool>(
                name: "Eliminated",
                table: "Teams",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STT",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeOfMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfMatches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeOfMatches");

            migrationBuilder.DropColumn(
                name: "View",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Eliminated",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "STT",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "round",
                table: "Matches");
        }
    }
}
