using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20248256 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelayDrivers_Delays",
                table: "DelayDrivers");

            migrationBuilder.AddForeignKey(
                name: "FK_DelayDrivers_Delays",
                table: "DelayDrivers",
                column: "DelayId",
                principalTable: "Delays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelayDrivers_Delays",
                table: "DelayDrivers");

            migrationBuilder.AddForeignKey(
                name: "FK_DelayDrivers_Delays",
                table: "DelayDrivers",
                column: "DelayId",
                principalTable: "Delays",
                principalColumn: "Id");
        }
    }
}
