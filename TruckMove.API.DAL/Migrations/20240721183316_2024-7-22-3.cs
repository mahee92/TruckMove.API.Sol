using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247223 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Correspondence",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Correspondence",
                table: "Jobs");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
