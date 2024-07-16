using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247146 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //

            //migrationBuilder.DropIndex(
            //    name: "IX_Notes_ChecklistId",
            //    table: "Notes");

            //

            migrationBuilder.DropIndex(
                name: "IX_Notes_PreDeparturechecklistId",
                table: "Notes");
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_PreDepartureChecklist",
                table: "Notes");

            //migrationBuilder.DropIndex(
            //    name: "IX_Notes_PreDeparturechecklistId",
            //    table: "Notes");

            migrationBuilder.DropColumn(
                name: "PreDeparturechecklistId",
                table: "Notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreDeparturechecklistId",
                table: "Notes",
                type: "int",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notes_PreDeparturechecklistId",
            //    table: "Notes",
            //    column: "PreDeparturechecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_PreDepartureChecklist",
                table: "Notes",
                column: "PreDeparturechecklistId",
                principalTable: "PreDepartureChecklist",
                principalColumn: "Id");
        }
    }
}
