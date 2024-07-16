using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247132 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicTransportId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PublicTransportId",
                table: "Notes",
                column: "PublicTransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PublicTransport",
                table: "Notes",
                column: "PublicTransportId",
                principalTable: "PublicTransport",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PublicTransport",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_PublicTransportId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "PublicTransportId",
                table: "Notes");
        }
    }
}
