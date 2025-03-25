using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class deletefromQA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromQA",
                table: "PaymentAdjustments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FromQA",
                table: "PaymentAdjustments",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))");
        }
    }
}
