using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "LegStatus",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Legs_DriverId",
                table: "Legs",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Users",
                table: "Legs",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Users",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_Legs_DriverId",
                table: "Legs");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "LegStatus",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
