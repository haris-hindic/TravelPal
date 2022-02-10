using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Database.Migrations
{
    public partial class addPaymentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentInfoId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CcNumber = table.Column<int>(type: "int", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CcvCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PaymentInfoId",
                table: "Reservations",
                column: "PaymentInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PaymentInfos_PaymentInfoId",
                table: "Reservations",
                column: "PaymentInfoId",
                principalTable: "PaymentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PaymentInfos_PaymentInfoId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "PaymentInfos");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PaymentInfoId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PaymentInfoId",
                table: "Reservations");
        }
    }
}
