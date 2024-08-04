using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024117 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accommodation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Assignee = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Driver = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodation_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accommodation_TaskStatus",
                        column: x => x.Status,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accommodation_Users",
                        column: x => x.Driver,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accommodation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accommodation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accommodation_Users1",
                        column: x => x.Assignee,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AccommodationId",
                table: "Notes",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_Assignee",
                table: "Accommodation",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_CreatedById",
                table: "Accommodation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_Driver",
                table: "Accommodation",
                column: "Driver");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_JobId",
                table: "Accommodation",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_Status",
                table: "Accommodation",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_UpdatedById",
                table: "Accommodation",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Accommodation",
                table: "Notes",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Accommodation",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Accommodation");

            migrationBuilder.DropIndex(
                name: "IX_Notes_AccommodationId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Notes");
        }
    }
}
