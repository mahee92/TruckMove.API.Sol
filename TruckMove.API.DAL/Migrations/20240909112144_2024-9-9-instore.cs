using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202499instore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "Id", "DarkColour", "Description", "LightColour", "Status" },
                values: new object[] { 15, "#8B4513", "A job is In Store", "#A0522D", "InStore" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
