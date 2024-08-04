using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247292 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganiseNow",
                table: "PermitsAndPlates");

            migrationBuilder.RenameColumn(
                name: "OrganiseNow",
                table: "Purchase",
                newName: "OrganizeNow");

            migrationBuilder.RenameColumn(
                name: "OrganiseNow",
                table: "Accommodation",
                newName: "OrganizeNow");

            migrationBuilder.AddColumn<bool>(
                name: "OrganizeNow",
                table: "PermitsAndPlates",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizeNow",
                table: "PermitsAndPlates");

            migrationBuilder.RenameColumn(
                name: "OrganizeNow",
                table: "Purchase",
                newName: "OrganiseNow");

            migrationBuilder.RenameColumn(
                name: "OrganizeNow",
                table: "Accommodation",
                newName: "OrganiseNow");

            migrationBuilder.AddColumn<bool>(
                name: "OrganiseNow",
                table: "PermitsAndPlates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
