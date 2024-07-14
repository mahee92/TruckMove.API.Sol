using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024714 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreDepartureChecklist_Jobs",
                table: "PreDepartureChecklist");

            migrationBuilder.DropIndex(
                name: "UQ_PreDepartureChecklist_JobId",
                table: "PreDepartureChecklist");

            migrationBuilder.AlterColumn<string>(
                name: "WindscreenDamageWipers",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Water",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VisuallyDipAndCheckTaps",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VehicleCleanFreeOfRubbish",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TyresCondition",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SpareRim",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RightHandDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RearDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnersManual",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Oil",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeftHandDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KeysFobTotalKeys",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JackAndTools",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PreDepartureChecklist",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(1)))");

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelLevel",
                table: "PreDepartureChecklist",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrontDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckTruckHeight",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInsideTruckTrailer",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AllLightsAndIndicators",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AirAndElectrics",
                table: "PreDepartureChecklist",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checklist",
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklist_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Checklist_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreDepartureChecklist_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreDepartureChecklist_JobId",
                table: "PreDepartureChecklist",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ChecklistId",
                table: "Notes",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_CreatedById",
                table: "Checklist",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_UpdatedById",
                table: "Checklist",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "UQ_PreDepartureChecklist_JobId",
                table: "Checklist",
                column: "JobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Checklist_ChecklistId",
                table: "Notes",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreDepartureChecklist_Jobs_JobId",
                table: "PreDepartureChecklist",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Checklist_ChecklistId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_PreDepartureChecklist_Jobs_JobId",
                table: "PreDepartureChecklist");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropIndex(
                name: "IX_PreDepartureChecklist_JobId",
                table: "PreDepartureChecklist");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ChecklistId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "WindscreenDamageWipers",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Water",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VisuallyDipAndCheckTaps",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VehicleCleanFreeOfRubbish",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TyresCondition",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SpareRim",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RightHandDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RearDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnersManual",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Oil",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeftHandDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KeysFobTotalKeys",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JackAndTools",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PreDepartureChecklist",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(1)))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelLevel",
                table: "PreDepartureChecklist",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrontDamage",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckTruckHeight",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInsideTruckTrailer",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AllLightsAndIndicators",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AirAndElectrics",
                table: "PreDepartureChecklist",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PreDepartureChecklist_JobId",
                table: "PreDepartureChecklist",
                column: "JobId",
                unique: true);
        }
    }
}
