using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _202494Rates3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false, defaultValueSql: "(CONVERT([float],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rates",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Rate paid per km for standard jobs", "Per_KM_Rate" },
                    { 2, "Maximum amount of KMs for a fixed cost job", "Max_fixed_job_KMs" },
                    { 3, "Standard fixed cost rate (4 hours at grade 4 rate)", "Fixed_job_rate" },
                    { 4, "Additional amount per KM paid when towing a commercial load", "Commercial_load_KM_rate" },
                    { 5, "Hourly rate paid in the event of a delay", "Delay_hourly_rate" },
                    { 6, "Per KM rate paid when towing dangerous goods", "Dangerous_Goods_day_Rate" },
                    { 7, "Hourly rate paid when traveling on public transport", "Public_transport_hourly_Rate" },
                    { 8, "Rate paid per km when working on a public holiday", "Public_holiday_KM_rate" },
                    { 9, "Rate paid per km when working on a Saturday", "Saturday_KM_rate" },
                    { 10, "Rate paid per km when working on a Sunday", "Sunday_KM_rate" },
                    { 11, "Public holiday fixed cost rate", "Public_holiday_fixed_rate" },
                    { 12, "Saturday fixed cost rate", "Saturday_fixed_rate" },
                    { 13, "Sunday fixed cost rate", "Sunday_fixed_rate" },
                    { 14, "Rate paid for a Single Hookup", "Hookup_Single" },
                    { 15, "Rate paid for a Double Hookup", "Hookup_Double" },
                    { 16, "Rate paid for a 4RA Hookup", "Hookup_4RA" },
                    { 17, "Anything under 500km", "Grade_4_Hourly_rate" },
                    { 18, "", "Grade_1_Hourly_rate_riding_on_public_transport" },
                    { 19, "", "Hookup_Road_Train" },
                    { 20, "If between 350 and 500 it becomes a 500 job", "Saturday_Hour_rate" },
                    { 21, "When under 350, then goes to hourly, minimum 4 hours", "Sunday_Hour_rate" },
                    { 22, "Hookup = driving with a trailer", "Holiday_hour_rate" },
                    { 23, "", "Public_Transport_Delay" },
                    { 24, "", "Breakdown_Delay" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
