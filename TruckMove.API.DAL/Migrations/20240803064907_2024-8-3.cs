using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202483 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "requiredsuburb",
                table: "PublicTransport",
                newName: "RequiredTosuburb");

            migrationBuilder.AddColumn<string>(
                name: "RequiredFromsuburb",
                table: "PublicTransport",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredFromsuburb",
                table: "PublicTransport");

            migrationBuilder.RenameColumn(
                name: "RequiredTosuburb",
                table: "PublicTransport",
                newName: "requiredsuburb");
        }
    }
}
