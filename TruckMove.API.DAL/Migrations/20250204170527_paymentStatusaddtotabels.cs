using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class paymentStatusaddtotabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "PublicTransport",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Legs",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "DelayDrivers",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PaymentStatus",
                table: "Purchase",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTransport_PaymentStatus",
                table: "PublicTransport",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_PaymentStatus",
                table: "Legs",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_DelayDrivers_PaymentStatus",
                table: "DelayDrivers",
                column: "PaymentStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_DelayDrivers_PaymentStatus",
                table: "DelayDrivers",
                column: "PaymentStatus",
                principalTable: "PaymentStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_PaymentStatus",
                table: "Legs",
                column: "PaymentStatus",
                principalTable: "PaymentStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicTransport_PaymentStatus",
                table: "PublicTransport",
                column: "PaymentStatus",
                principalTable: "PaymentStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PaymentStatus",
                table: "Purchase",
                column: "PaymentStatus",
                principalTable: "PaymentStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelayDrivers_PaymentStatus",
                table: "DelayDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Legs_PaymentStatus",
                table: "Legs");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicTransport_PaymentStatus",
                table: "PublicTransport");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PaymentStatus",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_PaymentStatus",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_PublicTransport_PaymentStatus",
                table: "PublicTransport");

            migrationBuilder.DropIndex(
                name: "IX_Legs_PaymentStatus",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_DelayDrivers_PaymentStatus",
                table: "DelayDrivers");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "PublicTransport");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "DelayDrivers");
        }
    }
}
