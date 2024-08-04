using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AccommodationId",
                table: "Attachments",
                column: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_Accommodation",
                table: "Attachments",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_Accommodation",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_AccommodationId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Attachments");
        }
    }
}
