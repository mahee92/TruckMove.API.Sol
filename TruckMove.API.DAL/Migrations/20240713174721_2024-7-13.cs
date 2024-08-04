using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024713 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicTransportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicTransportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicTransport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Driver = table.Column<int>(type: "int", nullable: true),
                    OrganizeNow = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Daterequired = table.Column<DateTime>(type: "datetime", nullable: true),
                    requiredsuburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assignee = table.Column<int>(type: "int", nullable: true),
                    BookingInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportType = table.Column<int>(type: "int", nullable: true),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    DepartureAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TransportCost = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicTransport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicTransport_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicTransport_PublicTransportTypes",
                        column: x => x.TransportType,
                        principalTable: "PublicTransportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicTransport_TaskStatus",
                        column: x => x.Status,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicTransport_Users",
                        column: x => x.Driver,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicTransport_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicTransport_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicTransport_Users1",
                        column: x => x.Assignee,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_Assignee",
                table: "PublicTransport",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_CreatedById",
                table: "PublicTransport",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_Driver",
                table: "PublicTransport",
                column: "Driver");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_JobId",
                table: "PublicTransport",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_Status",
                table: "PublicTransport",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_TransportType",
                table: "PublicTransport",
                column: "TransportType");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_UpdatedById",
                table: "PublicTransport",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicTransport");

            migrationBuilder.DropTable(
                name: "PublicTransportTypes");
        }
    }
}
