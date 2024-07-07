using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20246271 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LegStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    LegNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Legs_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Legs_LegStatus",
                        column: x => x.Status,
                        principalTable: "LegStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Legs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Legs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "LegStatus",
                columns: new[] { "Id", "Description", "Status" },
                values: new object[] { 1, null, "Planned" });

            migrationBuilder.InsertData(
                table: "LegStatus",
                columns: new[] { "Id", "Description", "Status" },
                values: new object[] { 2, null, "InProgress" });

            migrationBuilder.InsertData(
                table: "LegStatus",
                columns: new[] { "Id", "Description", "Status" },
                values: new object[] { 3, null, "Completed" });

            migrationBuilder.CreateIndex(
                name: "IX_Legs_CreatedById",
                table: "Legs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_JobId",
                table: "Legs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_Status",
                table: "Legs",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_UpdatedById",
                table: "Legs",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropTable(
                name: "LegStatus");
        }
    }
}
