using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024871 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DarkColour",
                table: "JobStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LightColour",
                table: "JobStatus",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);



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
                keyValue: 5,
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

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });

            migrationBuilder.UpdateData(
                table: "JobStatus",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DarkColour", "LightColour" },
                values: new object[] { "blue", "red" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarkColour",
                table: "JobStatus");

            migrationBuilder.DropColumn(
                name: "LightColour",
                table: "JobStatus");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Attachments");
        }
    }
}
