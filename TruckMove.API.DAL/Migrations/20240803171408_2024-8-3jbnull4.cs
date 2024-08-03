using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202483jbnull4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_Notes_Jobs",
              table: "Notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
