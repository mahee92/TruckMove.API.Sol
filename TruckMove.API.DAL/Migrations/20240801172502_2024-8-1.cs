using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202481 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Trailers",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Trailers",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Trailers",
                table: "Images",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Trailers",
                table: "Notes",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Trailers",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Trailers",
                table: "Notes");

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
    }
}
