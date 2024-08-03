using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202483jbnull3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Drop the existing constraint
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes");

            // Add a new foreign key with the desired behavior
            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Notes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Drop the new foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes");

            // Re-add the old foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Jobs",
                table: "Notes",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
