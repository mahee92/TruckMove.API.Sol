using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024783 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Attachments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Attachments",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(1)))");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Attachments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CreatedById",
                table: "Attachments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UpdatedById",
                table: "Attachments",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Users_CreatedById",
                table: "Attachments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Users_UpdatedById",
                table: "Attachments",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Users_CreatedById",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Users_UpdatedById",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_CreatedById",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_UpdatedById",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Attachments");
        }
    }
}
