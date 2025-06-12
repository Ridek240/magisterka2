using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magisterka2.Migrations
{
    /// <inheritdoc />
    public partial class Tagline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "riotIdGameName",
                table: "PlayerRanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "riotIdTagline",
                table: "PlayerRanks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "riotIdGameName",
                table: "PlayerRanks");

            migrationBuilder.DropColumn(
                name: "riotIdTagline",
                table: "PlayerRanks");
        }
    }
}
