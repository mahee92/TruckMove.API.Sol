using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202412143 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Acknowledgement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Acknowledgement_JobId",
                table: "Acknowledgement",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acknowledgement_Jobs",
                table: "Acknowledgement",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acknowledgement_Jobs",
                table: "Acknowledgement");

            migrationBuilder.DropIndex(
                name: "IX_Acknowledgement_JobId",
                table: "Acknowledgement");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Acknowledgement");
        }
    }
}
