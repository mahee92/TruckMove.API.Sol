using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202482 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_PublicTransport",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_Accommodation",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_PermitsAndPlates",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Accommodation",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PermitsAndPlates",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PublicTransport",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_PublicTransport",
                table: "Attachments",
                column: "PublicTransportId",
                principalTable: "PublicTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_Accommodation",
                table: "Attachments",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_PermitsAndPlates",
                table: "Attachments",
                column: "PermitAndPlateId",
                principalTable: "PermitsAndPlates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Accommodation",
                table: "Notes",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PermitsAndPlates",
                table: "Notes",
                column: "PermitAndPlatesId",
                principalTable: "PermitsAndPlates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PublicTransport",
                table: "Notes",
                column: "PublicTransportId",
                principalTable: "PublicTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_PublicTransport",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_Accommodation",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_PermitsAndPlates",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Accommodation",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PermitsAndPlates",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PublicTransport",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_PublicTransport",
                table: "Attachments",
                column: "PublicTransportId",
                principalTable: "PublicTransport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_Accommodation",
                table: "Attachments",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_PermitsAndPlates",
                table: "Attachments",
                column: "PermitAndPlateId",
                principalTable: "PermitsAndPlates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Accommodation",
                table: "Notes",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PermitsAndPlates",
                table: "Notes",
                column: "PermitAndPlatesId",
                principalTable: "PermitsAndPlates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PublicTransport",
                table: "Notes",
                column: "PublicTransportId",
                principalTable: "PublicTransport",
                principalColumn: "Id");
        }
    }
}
