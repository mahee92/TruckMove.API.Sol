using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class jobnNewClm_and_legTailer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HookupLeg",
                table: "Trailers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCommercialLoad",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDangerousGoods",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_HookupLeg",
                table: "Trailers",
                column: "HookupLeg");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailers_Legs",
                table: "Trailers",
                column: "HookupLeg",
                principalTable: "Legs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailers_Legs",
                table: "Trailers");

            migrationBuilder.DropIndex(
                name: "IX_Trailers_HookupLeg",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "HookupLeg",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "IsCommercialLoad",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsDangerousGoods",
                table: "Jobs");
        }
    }
}
