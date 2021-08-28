using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasuryYields.API.Migrations
{
    public partial class AddFromString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "TreasuryYieldsDays",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "TreasuryYieldsDays");
        }
    }
}
