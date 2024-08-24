using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20248241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#C0C0C0", "#E0E0E0" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#FFFFFF", "#FFFFFF" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#99CCFF", "#CCE5FF" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#CCFF99", "#E5FFCC" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#00FF00", "#00CC00" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#006600", "#009900" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#FF9933", "#FFB266" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#0080FF", "#3399FF" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "#0080FF", "#3399FF" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });
        }
    }
}
