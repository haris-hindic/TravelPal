using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Database.Migrations
{
    public partial class AddEventSignUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventSignUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventParticipantId = table.Column<int>(type: "int", nullable: false),
                    EventParticipantId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSignUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSignUps_AspNetUsers_EventParticipantId1",
                        column: x => x.EventParticipantId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSignUps_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSignUps_PaymentInfos_PaymentInfoId",
                        column: x => x.PaymentInfoId,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSignUps_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUps_EventId",
                table: "EventSignUps",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUps_EventParticipantId1",
                table: "EventSignUps",
                column: "EventParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUps_PaymentInfoId",
                table: "EventSignUps",
                column: "PaymentInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUps_StatusId",
                table: "EventSignUps",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventSignUps");
        }
    }
}
