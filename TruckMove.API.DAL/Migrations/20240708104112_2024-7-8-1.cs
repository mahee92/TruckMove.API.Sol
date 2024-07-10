using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024781 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PermitsAndPlates",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(1)))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "PermitsAndPlates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PermitsAndPlates_JobId",
                table: "PermitsAndPlates",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermitsAndPlates_Jobs",
                table: "PermitsAndPlates",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermitsAndPlates_Jobs",
                table: "PermitsAndPlates");

            migrationBuilder.DropIndex(
                name: "IX_PermitsAndPlates_JobId",
                table: "PermitsAndPlates");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "PermitsAndPlates");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PermitsAndPlates",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(1)))");
        }
    }
}
