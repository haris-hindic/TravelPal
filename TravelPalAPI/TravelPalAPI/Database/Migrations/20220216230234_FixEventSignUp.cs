using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Database.Migrations
{
    public partial class FixEventSignUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSignUps_AspNetUsers_EventParticipantId1",
                table: "EventSignUps");

            migrationBuilder.DropIndex(
                name: "IX_EventSignUps_EventParticipantId1",
                table: "EventSignUps");

            migrationBuilder.DropColumn(
                name: "EventParticipantId1",
                table: "EventSignUps");

            migrationBuilder.AlterColumn<string>(
                name: "EventParticipantId",
                table: "EventSignUps",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUps_EventParticipantId",
                table: "EventSignUps",
                column: "EventParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSignUps_AspNetUsers_EventParticipantId",
                table: "EventSignUps",
                column: "EventParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSignUps_AspNetUsers_EventParticipantId",
                table: "EventSignUps");

            migrationBuilder.DropIndex(
                name: "IX_EventSignUps_EventParticipantId",
                table: "EventSignUps");

            migrationBuilder.AlterColumn<int>(
                name: "EventParticipantId",
                table: "EventSignUps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventParticipantId1",
                table: "EventSignUps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUps_EventParticipantId1",
                table: "EventSignUps",
                column: "EventParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSignUps_AspNetUsers_EventParticipantId1",
                table: "EventSignUps",
                column: "EventParticipantId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
