using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasuryYields.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreasuryYieldsDays",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OneMonths = table.Column<double>(type: "double precision", nullable: true),
                    TwoMonths = table.Column<double>(type: "double precision", nullable: true),
                    ThreeMonths = table.Column<double>(type: "double precision", nullable: true),
                    SixMonths = table.Column<double>(type: "double precision", nullable: true),
                    OneYears = table.Column<double>(type: "double precision", nullable: true),
                    TwoYears = table.Column<double>(type: "double precision", nullable: true),
                    ThreeYears = table.Column<double>(type: "double precision", nullable: true),
                    FiveYears = table.Column<double>(type: "double precision", nullable: true),
                    SevenYears = table.Column<double>(type: "double precision", nullable: true),
                    TenYears = table.Column<double>(type: "double precision", nullable: true),
                    TwentyYears = table.Column<double>(type: "double precision", nullable: true),
                    ThirtyYears = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreasuryYieldsDays", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreasuryYieldsDays");
        }
    }
}
