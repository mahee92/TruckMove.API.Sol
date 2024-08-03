using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202483jbnull5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Re-add the old foreign key
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

        }
    }
}
