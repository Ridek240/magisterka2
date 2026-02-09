using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magisterka2.Migrations
{
    /// <inheritdoc />
    public partial class Validacja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ValidationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlobalAccuracy = table.Column<double>(type: "float", nullable: false),
                    TimeMs = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfusionMatrixEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrueIndex = table.Column<int>(type: "int", nullable: false),
                    PredictedIndex = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ValidationResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfusionMatrixEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfusionMatrixEntries_ValidationResults_ValidationResultId",
                        column: x => x.ValidationResultId,
                        principalTable: "ValidationResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutcomeResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutcomeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutcomeIndex = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<double>(type: "float", nullable: false),
                    ValidationResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutcomeResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutcomeResults_ValidationResults_ValidationResultId",
                        column: x => x.ValidationResultId,
                        principalTable: "ValidationResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RocPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutcomeIndex = table.Column<int>(type: "int", nullable: false),
                    Fpr = table.Column<double>(type: "float", nullable: false),
                    Tpr = table.Column<double>(type: "float", nullable: false),
                    ValidationResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RocPoints_ValidationResults_ValidationResultId",
                        column: x => x.ValidationResultId,
                        principalTable: "ValidationResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfusionMatrixEntries_ValidationResultId",
                table: "ConfusionMatrixEntries",
                column: "ValidationResultId");

            migrationBuilder.CreateIndex(
                name: "IX_OutcomeResults_ValidationResultId",
                table: "OutcomeResults",
                column: "ValidationResultId");

            migrationBuilder.CreateIndex(
                name: "IX_RocPoints_ValidationResultId",
                table: "RocPoints",
                column: "ValidationResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfusionMatrixEntries");

            migrationBuilder.DropTable(
                name: "OutcomeResults");

            migrationBuilder.DropTable(
                name: "RocPoints");

            migrationBuilder.DropTable(
                name: "ValidationResults");
        }
    }
}
