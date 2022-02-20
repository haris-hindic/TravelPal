using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPalAPI.Database.Migrations
{
    public partial class ccNumbertostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CcNumber",
                table: "PaymentInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
  

            migrationBuilder.AlterColumn<int>(
                name: "CcNumber",
                table: "PaymentInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
