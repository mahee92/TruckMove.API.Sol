using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20248252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Delays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Delays",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Delays",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(1)))");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Delays",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Delays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delays_CreatedById",
                table: "Delays",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Delays_UpdatedById",
                table: "Delays",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Delays_Users_CreatedById",
                table: "Delays",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delays_Users_UpdatedById",
                table: "Delays",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delays_Users_CreatedById",
                table: "Delays");

            migrationBuilder.DropForeignKey(
                name: "FK_Delays_Users_UpdatedById",
                table: "Delays");

            migrationBuilder.DropIndex(
                name: "IX_Delays_CreatedById",
                table: "Delays");

            migrationBuilder.DropIndex(
                name: "IX_Delays_UpdatedById",
                table: "Delays");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Delays");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Delays");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Delays");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Delays");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Delays");
        }
    }
}
