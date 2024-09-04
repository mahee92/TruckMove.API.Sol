using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202494Rates5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Rates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Rates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Rates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Rates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CreatedById",
                table: "Rates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UpdatedById",
                table: "Rates",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_CreatedById",
                table: "Rates",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_UpdatedById",
                table: "Rates",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_CreatedById",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_UpdatedById",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_CreatedById",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UpdatedById",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Rates");
        }
    }
}
