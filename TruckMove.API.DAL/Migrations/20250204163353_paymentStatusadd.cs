using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class paymentStatusadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "QAPending" },
                    { 2, "QADone" },
                    { 3, "Verified" },
                    { 4, "PaymentDone" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentStatus");
        }
    }
}
