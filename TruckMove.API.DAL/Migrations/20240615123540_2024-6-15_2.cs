using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024615_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_VehicleId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UQ_Vehicles_JobId",
                table: "Vehicles",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Jobs_VehicleId",
                table: "Jobs",
                column: "VehicleId",
                unique: true,
                filter: "[VehicleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Jobs_JobId",
                table: "Vehicles",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Jobs_JobId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "UQ_Vehicles_JobId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "UQ_Jobs_VehicleId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Vehicles");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_VehicleId",
                table: "Jobs",
                column: "VehicleId");
        }
    }
}
