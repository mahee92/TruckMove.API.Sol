using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class delayType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DelayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "TripDelay");

            migrationBuilder.UpdateData(
                table: "DelayTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: "PublicTransportDelay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DelayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Delay occoured in Trip (breakdown ext.)");

            migrationBuilder.UpdateData(
                table: "DelayTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: "Delay occoured in public traspotation");
        }
    }
}
