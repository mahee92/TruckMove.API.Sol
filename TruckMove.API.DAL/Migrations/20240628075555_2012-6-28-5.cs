using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _20126285 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HookupTypes",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[] { 1, "HU Single", "HU_Single" });

            migrationBuilder.InsertData(
                table: "HookupTypes",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[] { 2, "HU Double", "HU_Double" });

            migrationBuilder.InsertData(
                table: "HookupTypes",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[] { 3, "4RA (4 Rigid Axle )", "FOUR_RA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HookupTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HookupTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HookupTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
