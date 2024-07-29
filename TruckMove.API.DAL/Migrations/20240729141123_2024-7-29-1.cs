using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20247291 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OrganizeNow",
                table: "PublicTransport",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OrganizeNow",
                table: "PublicTransport",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
