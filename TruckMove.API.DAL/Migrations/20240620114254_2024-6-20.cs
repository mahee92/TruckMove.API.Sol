using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024620 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Driver",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EstimatedDaysofTravel",
                table: "Jobs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "Jobs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalDistance",
                table: "Jobs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalDrivingTime",
                table: "Jobs",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleName",
                value: "Driver");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Driver",
                table: "Jobs",
                column: "Driver");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users",
                table: "Jobs",
                column: "Driver",
                principalTable: "Users",
                principalColumn: "Id");
           
            migrationBuilder.AddColumn<DateTime>(
               name: "EstimatedDeliveryDate",
               table: "Jobs",
               type: "datetime",
               nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_Driver",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Driver",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EstimatedDaysofTravel",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalDistance",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalDrivingTime",
                table: "Jobs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleName",
                value: "Drivers");

            migrationBuilder.DropColumn(
               name: "EstimatedDeliveryDate",
               table: "Jobs");
        }
    }
}
