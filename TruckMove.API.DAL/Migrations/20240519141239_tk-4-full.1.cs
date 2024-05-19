using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class tk4full1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId1",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserModelId",
                table: "Contacts",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserModelId1",
                table: "Contacts",
                column: "UserModelId1");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserModelId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserModelId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserModelId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserModelId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserModelId1",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
