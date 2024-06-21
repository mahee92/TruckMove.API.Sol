using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20246202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "WayPoints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WayPoints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WayPoints",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "WayPoints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "WayPoints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WayPoints_CreatedById",
                table: "WayPoints",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_WayPoints_UpdatedById",
                table: "WayPoints",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_WayPoints_Users_CreatedById",
                table: "WayPoints",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WayPoints_Users_UpdatedById",
                table: "WayPoints",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WayPoints_Users_CreatedById",
                table: "WayPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_WayPoints_Users_UpdatedById",
                table: "WayPoints");

            migrationBuilder.DropIndex(
                name: "IX_WayPoints_CreatedById",
                table: "WayPoints");

            migrationBuilder.DropIndex(
                name: "IX_WayPoints_UpdatedById",
                table: "WayPoints");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "WayPoints");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WayPoints");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WayPoints");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "WayPoints");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "WayPoints");
        }
    }
}
