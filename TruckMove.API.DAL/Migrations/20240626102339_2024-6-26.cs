using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024626 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    TrailerId = table.Column<int>(type: "int", nullable: true),
                    PreDeparturechecklistId = table.Column<int>(type: "int", nullable: true),
                    VisibletoDriver = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_PreDepartureChecklist",
                        column: x => x.PreDeparturechecklistId,
                        principalTable: "PreDepartureChecklist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Vehicles",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_CreatedById",
                table: "Note",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Note_JobId",
                table: "Note",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_PreDeparturechecklistId",
                table: "Note",
                column: "PreDeparturechecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UpdatedById",
                table: "Note",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Note_VehicleId",
                table: "Note",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note");
        }
    }
}
