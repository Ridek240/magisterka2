using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magisterka2.Migrations
{
    /// <inheritdoc />
    public partial class why2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CERank",
                table: "DiagnosticResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MPCRank",
                table: "DiagnosticResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NCERank",
                table: "DiagnosticResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CERank",
                table: "DiagnosticResults");

            migrationBuilder.DropColumn(
                name: "MPCRank",
                table: "DiagnosticResults");

            migrationBuilder.DropColumn(
                name: "NCERank",
                table: "DiagnosticResults");
        }
    }
}
