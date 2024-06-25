using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20246241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WayPoints_Users_CreatedById",
            //    table: "WayPoints");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WayPoints_Users_UpdatedById",
            //    table: "WayPoints");

            //migrationBuilder.DropIndex(
            //    name: "IX_WayPoints_CreatedById",
            //    table: "WayPoints");

            //migrationBuilder.DropIndex(
            //    name: "IX_WayPoints_UpdatedById",
            //    table: "WayPoints");

            //migrationBuilder.DropColumn(
            //    name: "CreatedById",
            //    table: "WayPoints");

            //migrationBuilder.DropColumn(
            //    name: "CreatedDate",
            //    table: "WayPoints");

            //migrationBuilder.DropColumn(
            //    name: "IsActive",
            //    table: "WayPoints");

            //migrationBuilder.DropColumn(
            //    name: "LastModifiedDate",
            //    table: "WayPoints");

            //migrationBuilder.DropColumn(
            //    name: "UpdatedById",
            //    table: "WayPoints");

            migrationBuilder.AddColumn<int>(
                name: "PreDepatureCheckListId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PreDepartureChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Water = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SpareRim = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AllLightsAndIndicators = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    JackAndTools = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OwnersManual = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AirAndElectrics = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TyresCondition = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VisuallyDipAndCheckTaps = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    WindscreenDamageWipers = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VehicleCleanFreeOfRubbish = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    KeysFobTotalKeys = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CheckInsideTruckTrailer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Oil = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CheckTruckHeight = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LeftHandDamage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RightHandDamage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FrontDamage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RearDamage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NotesId = table.Column<int>(type: "int", nullable: true),
                    PhotosId = table.Column<int>(type: "int", nullable: true),
                    FuelLevel = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreDepartureChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreDepartureChecklist_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreDepartureChecklist_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreDepartureChecklist_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreDepartureChecklist_CreatedById",
                table: "PreDepartureChecklist",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PreDepartureChecklist_UpdatedById",
                table: "PreDepartureChecklist",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "UQ_PreDepartureChecklist_JobId",
                table: "PreDepartureChecklist",
                column: "JobId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreDepartureChecklist");

            migrationBuilder.DropColumn(
                name: "PreDepatureCheckListId",
                table: "Jobs");

            //migrationBuilder.AddColumn<int>(
            //    name: "CreatedById",
            //    table: "WayPoints",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "WayPoints",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "WayPoints",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "LastModifiedDate",
            //    table: "WayPoints",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "UpdatedById",
            //    table: "WayPoints",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_WayPoints_CreatedById",
            //    table: "WayPoints",
            //    column: "CreatedById");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WayPoints_UpdatedById",
            //    table: "WayPoints",
            //    column: "UpdatedById");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WayPoints_Users_CreatedById",
            //    table: "WayPoints",
            //    column: "CreatedById",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WayPoints_Users_UpdatedById",
            //    table: "WayPoints",
            //    column: "UpdatedById",
            //    principalTable: "Users",
            //    principalColumn: "Id");
        }
    }
}
