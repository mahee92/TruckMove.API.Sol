using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "FromMobile",
                table: "Purchase",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FromMobile",
                table: "Purchase",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");
        }
    }
}
