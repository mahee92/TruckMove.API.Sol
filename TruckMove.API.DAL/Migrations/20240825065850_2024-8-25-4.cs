using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20248254 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DelayId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_DelayId",
                table: "Notes",
                column: "DelayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Delays",
                table: "Notes",
                column: "DelayId",
                principalTable: "Delays",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Delays",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_DelayId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "DelayId",
                table: "Notes");
        }
    }
}
