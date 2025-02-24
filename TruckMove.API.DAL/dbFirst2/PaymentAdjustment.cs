using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class PaymentAdjustment
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int DriverId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Status { get; set; }

        public virtual User Driver { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual PaymentStatus StatusNavigation { get; set; } = null!;
    }
}
