using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247148 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Checklist",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Checklist",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Checklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Checklist",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Checklist",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_CreatedById",
                table: "Checklist",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_UpdatedById",
                table: "Checklist",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Users_CreatedById",
                table: "Checklist",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Users_UpdatedById",
                table: "Checklist",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Users_CreatedById",
                table: "Checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Users_UpdatedById",
                table: "Checklist");

            migrationBuilder.DropIndex(
                name: "IX_Checklist_CreatedById",
                table: "Checklist");

            migrationBuilder.DropIndex(
                name: "IX_Checklist_UpdatedById",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Checklist");
        }
    }
}
