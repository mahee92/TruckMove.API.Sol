using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20246301 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notes_TrailerId",
                table: "Notes",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TrailerId",
                table: "Images",
                column: "TrailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Trailers",
                table: "Images",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Trailers",
                table: "Notes",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Trailers",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Trailers",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_TrailerId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Images_TrailerId",
                table: "Images");
        }
    }
}
