using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247163 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreDepatureCheckListId",
                table: "Jobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreDepatureCheckListId",
                table: "Jobs",
                type: "int",
                nullable: true);
        }
    }
}
