using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024210trailerpick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Trailers",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.CreateTable(
                name: "TrailerStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TrailerStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 0, "NotPicked" });

            migrationBuilder.InsertData(
                table: "TrailerStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 1, "Picked" });

            migrationBuilder.InsertData(
                table: "TrailerStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 2, "Droppped" });

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_Status",
                table: "Trailers",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailers_TrailerStatus",
                table: "Trailers",
                column: "Status",
                principalTable: "TrailerStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailers_TrailerStatus",
                table: "Trailers");

            migrationBuilder.DropTable(
                name: "TrailerStatus");

            migrationBuilder.DropIndex(
                name: "IX_Trailers_Status",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trailers");
        }
    }
}
