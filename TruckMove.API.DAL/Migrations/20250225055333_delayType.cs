using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class delayType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DelayTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DelayTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Delay occoured in Trip (breakdown ext.)" });

            migrationBuilder.InsertData(
                table: "DelayTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Delay occoured in public traspotation" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelayTypes");
        }
    }
}
