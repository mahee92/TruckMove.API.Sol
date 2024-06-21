using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024621 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "Id", "Description", "Status" },
                values: new object[,]
                {
                    { 1, "A job that has been created in the system but does not have the minimum required information to complete the booking", "Planned" },
                    { 2, "A job that has the minimum required information (pickup location, dropoff location, vehicle information, assigned driver)", "Booked" },
                    { 3, "A booked job that is on or passed the pickup date.", "ReadyForPickup" },
                    { 4, "Status once the driver has arrived to pick up the truck and is done the pre departure check", "PreDepartureChecked" },
                    { 6, "Driver has completed the acknowledgement ", "Acknowledged" },
                    { 7, "A job that is currently in progress", "InProgress" },
                    { 8, "status when driver stops for the night", "Stopped" },
                    { 9, "status when driver stops for the night", "Delayed" },
                    { 10, "A job that has arrived at the destination", "Arrived" },
                    { 11, "status when driver is competed arrival checklist", "ArrivalChecked" },
                    { 12, "QA completed", "QADone" },
                    { 13, "Payment Done", "PaymentDone" },
                    { 14, "Billing Done", "BillingDone" },
                    { 15, "A job that has been completed successfully", "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Status",
                table: "Jobs",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobStatus",
                table: "Jobs",
                column: "Status",
                principalTable: "JobStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobStatus",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_Status",
                table: "Jobs");
        }
    }
}
