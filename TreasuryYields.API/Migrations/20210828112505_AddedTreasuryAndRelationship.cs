using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasuryYields.API.Migrations
{
    public partial class AddedTreasuryAndRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "TreasuryYieldsDays");

            migrationBuilder.AddColumn<Guid>(
                name: "TreasuryID",
                table: "TreasuryYieldsDays",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Treasuries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Agency = table.Column<string>(type: "text", nullable: true),
                    Seal = table.Column<string>(type: "text", nullable: true),
                    Alpha2Code = table.Column<string>(type: "text", nullable: true),
                    Alpha3Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treasuries", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Treasuries",
                columns: new[] { "ID", "Agency", "Alpha2Code", "Alpha3Code", "Country", "Seal" },
                values: new object[] { new Guid("32374aa6-0ea4-4ae8-be44-ee7e46d94f8f"), "U.S. Department of the Treasury", "US", "USA", "United States of America", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Seal_of_the_United_States_Department_of_the_Treasury.svg/1024px-Seal_of_the_United_States_Department_of_the_Treasury.svg.png" });

            migrationBuilder.CreateIndex(
                name: "IX_TreasuryYieldsDays_TreasuryID",
                table: "TreasuryYieldsDays",
                column: "TreasuryID");

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryYieldsDays_Treasuries_TreasuryID",
                table: "TreasuryYieldsDays",
                column: "TreasuryID",
                principalTable: "Treasuries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryYieldsDays_Treasuries_TreasuryID",
                table: "TreasuryYieldsDays");

            migrationBuilder.DropTable(
                name: "Treasuries");

            migrationBuilder.DropIndex(
                name: "IX_TreasuryYieldsDays_TreasuryID",
                table: "TreasuryYieldsDays");

            migrationBuilder.DropColumn(
                name: "TreasuryID",
                table: "TreasuryYieldsDays");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "TreasuryYieldsDays",
                type: "text",
                nullable: true);
        }
    }
}
