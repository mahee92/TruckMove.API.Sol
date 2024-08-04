using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247167 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_PreDepartureChecklist_JobId",
                table: "Checklist");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_JobId",
                table: "Checklist",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Checklist_JobId",
                table: "Checklist");

            migrationBuilder.CreateIndex(
                name: "UQ_PreDepartureChecklist_JobId",
                table: "Checklist",
                column: "JobId",
                unique: true);
        }
    }
}
