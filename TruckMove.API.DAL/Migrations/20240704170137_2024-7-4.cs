using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202474 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "NoteText",
            //    table: "Notes",
            //    newName: "Note1");

            migrationBuilder.AddColumn<int>(
                name: "PermitAndPlatesId",
                table: "Notes",
                type: "int",
                nullable: true);

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
                name: "TaskStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermitsAndPlates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrganiseNow = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Assignee = table.Column<int>(type: "int", nullable: true),
                    PermitNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostForPermit = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitsAndPlates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitsAndPlates_TaskStatus",
                        column: x => x.Status,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PermitsAndPlates_Users",
                        column: x => x.Assignee,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PermitsAndPlates_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PermitsAndPlates_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PermitAndPlatesId",
                table: "Notes",
                column: "PermitAndPlatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_Variance",
                table: "Legs",
                column: "Variance");

            migrationBuilder.CreateIndex(
                name: "IX_PermitsAndPlate_CreatedById",
                table: "PermitsAndPlates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PermitsAndPlate_UpdatedById",
                table: "PermitsAndPlates",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PermitsAndPlates_Assignee",
                table: "PermitsAndPlates",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_PermitsAndPlates_Status",
                table: "PermitsAndPlates",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Variances",
                table: "Legs",
                column: "Variance",
                principalTable: "Variances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PermitsAndPlates",
                table: "Notes",
                column: "PermitAndPlatesId",
                principalTable: "PermitsAndPlates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Variances",
                table: "Legs");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PermitsAndPlates",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "PermitsAndPlates");

            migrationBuilder.DropTable(
                name: "Variances");

            migrationBuilder.DropTable(
                name: "TaskStatus");

            migrationBuilder.DropIndex(
                name: "IX_Notes_PermitAndPlatesId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Legs_Variance",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "PermitAndPlatesId",
                table: "Notes");

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

            migrationBuilder.RenameColumn(
                name: "Note1",
                table: "Notes",
                newName: "NoteText");
        }
    }
}
