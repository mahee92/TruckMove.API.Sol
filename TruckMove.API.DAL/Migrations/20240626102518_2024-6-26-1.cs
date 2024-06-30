using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20246261 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Users_CreatedById",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Users_UpdatedById",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "IX_Note_VehicleId",
                table: "Notes",
                newName: "IX_Notes_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_UpdatedById",
                table: "Notes",
                newName: "IX_Notes_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Note_PreDeparturechecklistId",
                table: "Notes",
                newName: "IX_Notes_PreDeparturechecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_JobId",
                table: "Notes",
                newName: "IX_Notes_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_CreatedById",
                table: "Notes",
                newName: "IX_Notes_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_CreatedById",
                table: "Notes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UpdatedById",
                table: "Notes",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_CreatedById",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UpdatedById",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Note");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_VehicleId",
                table: "Note",
                newName: "IX_Note_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UpdatedById",
                table: "Note",
                newName: "IX_Note_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_PreDeparturechecklistId",
                table: "Note",
                newName: "IX_Note_PreDeparturechecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_JobId",
                table: "Note",
                newName: "IX_Note_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_CreatedById",
                table: "Note",
                newName: "IX_Note_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Users_CreatedById",
                table: "Note",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Users_UpdatedById",
                table: "Note",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
