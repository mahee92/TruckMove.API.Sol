using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024721 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicTransportId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_PublicTransportId",
                table: "Attachments",
                column: "PublicTransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_PublicTransport",
                table: "Attachments",
                column: "PublicTransportId",
                principalTable: "PublicTransport",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_PublicTransport",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_PublicTransportId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "PublicTransportId",
                table: "Attachments");
        }
    }
}
