using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024674 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs_1",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Jobs",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Jobs",
                newName: "JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs_1",
                table: "Jobs",
                column: "JobId");
        }
    }
}
