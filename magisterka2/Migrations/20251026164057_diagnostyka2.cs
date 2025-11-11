using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magisterka2.Migrations
{
    /// <inheritdoc />
    public partial class diagnostyka2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetValue",
                table: "DiagnosticResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetValue",
                table: "DiagnosticResults");
        }
    }
}
