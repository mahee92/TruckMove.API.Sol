using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20249711 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_Users_CreatedById",
                table: "TaskAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_Users_UpdatedById",
                table: "TaskAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskAttachments",
                table: "TaskAttachments");

            migrationBuilder.RenameTable(
                name: "TaskAttachments",
                newName: "Attachments");

            migrationBuilder.RenameIndex(
                name: "IX_TaskAttachments_UpdatedById",
                table: "Attachments",
                newName: "IX_Attachments_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TaskAttachments_PermitAndPlateId",
                table: "Attachments",
                newName: "IX_Attachments_PermitAndPlateId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskAttachments_CreatedById",
                table: "Attachments",
                newName: "IX_Attachments_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "TaskAttachments");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_UpdatedById",
                table: "TaskAttachments",
                newName: "IX_TaskAttachments_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_PermitAndPlateId",
                table: "TaskAttachments",
                newName: "IX_TaskAttachments_PermitAndPlateId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_CreatedById",
                table: "TaskAttachments",
                newName: "IX_TaskAttachments_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskAttachments",
                table: "TaskAttachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_Users_CreatedById",
                table: "TaskAttachments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_Users_UpdatedById",
                table: "TaskAttachments",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
