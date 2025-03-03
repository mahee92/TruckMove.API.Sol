using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class delayTypeadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Delays",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.CreateIndex(
                name: "IX_Delays_Type",
                table: "Delays",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Delays_DelayTypes",
                table: "Delays",
                column: "Type",
                principalTable: "DelayTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delays_DelayTypes",
                table: "Delays");

            migrationBuilder.DropIndex(
                name: "IX_Delays_Type",
                table: "Delays");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Delays");
        }
    }
}
