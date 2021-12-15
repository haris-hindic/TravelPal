using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Migrations
{
    public partial class bind_user_to_accommodation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HostId",
                table: "Accommodations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_HostId",
                table: "Accommodations",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_AspNetUsers_HostId",
                table: "Accommodations",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_AspNetUsers_HostId",
                table: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_HostId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Accommodations");
        }
    }
}
