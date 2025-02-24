using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class PaymentAdjustment : AuditableEntity
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
