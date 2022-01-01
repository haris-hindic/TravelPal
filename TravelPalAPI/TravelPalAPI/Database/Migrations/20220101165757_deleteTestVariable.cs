using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Database.Migrations
{
    public partial class deleteTestVariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMigracije",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "AccommodationImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestMigracije",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "AccommodationImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
