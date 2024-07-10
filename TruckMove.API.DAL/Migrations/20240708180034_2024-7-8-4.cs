using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024784 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 1, "Planned" });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 2, "InProgress" });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 3, "Completed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
