using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class tk4full2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserModelId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserModelId1",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "UserModelId1",
                table: "Contacts",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "UserModelId",
                table: "Contacts",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UserModelId1",
                table: "Contacts",
                newName: "IX_Contacts_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UserModelId",
                table: "Contacts",
                newName: "IX_Contacts_CreatedById");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_CreatedById",
                table: "Contacts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UpdatedById",
                table: "Contacts",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_CreatedById",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UpdatedById",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Contacts",
                newName: "UserModelId1");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Contacts",
                newName: "UserModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UpdatedById",
                table: "Contacts",
                newName: "IX_Contacts_UserModelId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_CreatedById",
                table: "Contacts",
                newName: "IX_Contacts_UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserModelId",
                table: "Contacts",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserModelId1",
                table: "Contacts",
                column: "UserModelId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
