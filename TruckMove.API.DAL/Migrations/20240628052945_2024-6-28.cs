using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024628 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acknowledgement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegId = table.Column<int>(type: "int", nullable: false),
                    Acknowledge = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acknowledgement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acknowledgement_Legs",
                        column: x => x.LegId,
                        principalTable: "Legs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Acknowledge_LegId",
                table: "Acknowledgement",
                column: "LegId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acknowledgement");
        }
    }
}
