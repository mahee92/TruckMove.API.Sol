using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20248251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Assignee = table.Column<int>(type: "int", nullable: true),
                    OrganizeNow = table.Column<bool>(type: "bit", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delays_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Delays_TaskStatus",
                        column: x => x.Status,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Delays_Users",
                        column: x => x.Assignee,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DelayDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelayId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DelayDrivers_Delays",
                        column: x => x.DelayId,
                        principalTable: "Delays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DelayDrivers_Users",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelayDrivers_DelayId",
                table: "DelayDrivers",
                column: "DelayId");

            migrationBuilder.CreateIndex(
                name: "IX_DelayDrivers_DriverId",
                table: "DelayDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Delays_Assignee",
                table: "Delays",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_Delays_JobId",
                table: "Delays",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Delays_Status",
                table: "Delays",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelayDrivers");

            migrationBuilder.DropTable(
                name: "Delays");
        }
    }
}
