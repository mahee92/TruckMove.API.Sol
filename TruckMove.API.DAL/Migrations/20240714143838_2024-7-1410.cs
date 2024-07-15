using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202471410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ChecklistId",
                table: "Notes",
                column: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Checklist",
                table: "Notes",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Checklist",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ChecklistId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "Notes");
        }
    }
}
