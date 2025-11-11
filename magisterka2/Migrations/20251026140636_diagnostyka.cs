using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magisterka2.Migrations
{
    /// <inheritdoc />
    public partial class diagnostyka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiagnosticResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Node = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MPC = table.Column<double>(type: "float", nullable: false),
                    CE = table.Column<double>(type: "float", nullable: false),
                    NCE = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticResults");
        }
    }
}
