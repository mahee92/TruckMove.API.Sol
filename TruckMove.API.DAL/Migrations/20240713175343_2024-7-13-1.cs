using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PublicTransportTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Train" },
                    { 2, "Plane" },
                    { 3, "Taxi" },
                    { 4, "Uber" },
                    { 5, "Other" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PublicTransportTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PublicTransportTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PublicTransportTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PublicTransportTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PublicTransportTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
