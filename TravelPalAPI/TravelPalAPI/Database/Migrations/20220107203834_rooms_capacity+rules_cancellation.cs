using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Database.Migrations
{
    public partial class rooms_capacityrules_cancellation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Accommodations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Accommodations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cancellation",
                table: "AccommodationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRules",
                table: "AccommodationDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Cancellation",
                table: "AccommodationDetails");

            migrationBuilder.DropColumn(
                name: "HouseRules",
                table: "AccommodationDetails");
        }
    }
}
