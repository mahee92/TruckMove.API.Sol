using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202497dealy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Delays",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Delays",
                table: "Notes",
                column: "DelayId",
                principalTable: "Delays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Delays",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Delays",
                table: "Notes",
                column: "DelayId",
                principalTable: "Delays",
                principalColumn: "Id");
        }
    }
}
