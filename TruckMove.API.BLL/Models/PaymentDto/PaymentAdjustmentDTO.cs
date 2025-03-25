namespace TruckMove.API.BLL.Models.PaymentDto
{
    public class PaymentAdjustmentDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int DriverId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public bool? IsFromQa { get; set; }
    }
}