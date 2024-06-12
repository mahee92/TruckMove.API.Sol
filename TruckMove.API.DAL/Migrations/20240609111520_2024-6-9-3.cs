using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024693 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "JobContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "JobContacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "JobContacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "JobContacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobContacts_CreatedById",
                table: "JobContacts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobContacts_UpdatedById",
                table: "JobContacts",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContacts_Users_CreatedById",
                table: "JobContacts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContacts_Users_UpdatedById",
                table: "JobContacts",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContacts_Users_CreatedById",
                table: "JobContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_JobContacts_Users_UpdatedById",
                table: "JobContacts");

            migrationBuilder.DropIndex(
                name: "IX_JobContacts_CreatedById",
                table: "JobContacts");

            migrationBuilder.DropIndex(
                name: "IX_JobContacts_UpdatedById",
                table: "JobContacts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "JobContacts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "JobContacts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobContacts");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "JobContacts");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "JobContacts");
        }
    }
}
