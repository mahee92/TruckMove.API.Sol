using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202427 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Legs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(1)))");

            migrationBuilder.AddColumn<string>(
                name: "EndLocation",
                table: "Legs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Legs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartLocation",
                table: "Legs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Legs",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TotalDistance",
                table: "Legs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Variance",
                table: "Legs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Variances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variances", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Variances",
                columns: new[] { "Id", "Description", "Name", "Rate" },
                values: new object[,]
                {
                    { 0, "Default", "_default", null },
                    { 1, "DG", "DG", null },
                    { 2, "Sat Rate", "Sat_rate", null },
                    { 3, "Sun Rate", "Sun_rate", null },
                    { 4, "G7", "G7", null },
                    { 5, "Public Holiday", "Public_Holiday", null },
                    { 6, "G4", "G4", null },
                    { 7, "Booking (Bullbar)", "Bookining_Bullbar", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variances");

            migrationBuilder.DropColumn(
                name: "EndLocation",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "StartLocation",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "TotalDistance",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "Variance",
                table: "Legs");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Legs",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(1)))",
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
