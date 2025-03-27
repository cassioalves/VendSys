using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendSys.Migrations
{
    /// <inheritdoc />
    public partial class AddDEXMeterAndLaneMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEXMeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Machine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DEXDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MachineSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueOfPaidVends = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEXMeters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DEXLaneMeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DEXMeterId = table.Column<int>(type: "int", nullable: false),
                    ProductIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfVends = table.Column<int>(type: "int", nullable: false),
                    ValueOfPaidSalves = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEXLaneMeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEXLaneMeters_DEXMeters_DEXMeterId",
                        column: x => x.DEXMeterId,
                        principalTable: "DEXMeters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DEXLaneMeters_DEXMeterId",
                table: "DEXLaneMeters",
                column: "DEXMeterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DEXLaneMeters");

            migrationBuilder.DropTable(
                name: "DEXMeters");
        }
    }
}
