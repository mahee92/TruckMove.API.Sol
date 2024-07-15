using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247152 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Driver = table.Column<int>(type: "int", nullable: true),
                    FromMobile = table.Column<int>(type: "int", nullable: false),
                    OrganiseNow = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Assignee = table.Column<int>(type: "int", nullable: true),
                    ReciptUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFuel = table.Column<bool>(type: "bit", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Liters = table.Column<double>(type: "float", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_TaskStatus",
                        column: x => x.Status,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Users",
                        column: x => x.Driver,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Users1",
                        column: x => x.Assignee,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Assignee",
                table: "Purchase",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CreatedById",
                table: "Purchase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Driver",
                table: "Purchase",
                column: "Driver");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_JobId",
                table: "Purchase",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Status",
                table: "Purchase",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_UpdatedById",
                table: "Purchase",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase");
        }
    }
}
