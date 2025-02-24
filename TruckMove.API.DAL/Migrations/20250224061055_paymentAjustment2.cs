using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class paymentAjustment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "PaymentAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAdjustments_Jobs",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentAdjustments_PaymentStatus",
                        column: x => x.Status,
                        principalTable: "PaymentStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentAdjustments_Users",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentAdjustments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentAdjustments_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });




            migrationBuilder.CreateIndex(
                name: "IX_PaymentAdjustments_CreatedById",
                table: "PaymentAdjustments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAdjustments_DriverId",
                table: "PaymentAdjustments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAdjustments_JobId",
                table: "PaymentAdjustments",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAdjustments_Status",
                table: "PaymentAdjustments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAdjustments_UpdatedById",
                table: "PaymentAdjustments",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentAdjustments");


        }
    }
}
