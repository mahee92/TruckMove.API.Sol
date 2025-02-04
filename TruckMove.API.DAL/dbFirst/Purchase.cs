using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public int? Driver { get; set; }
        public bool OrganizeNow { get; set; }
        public int? Assignee { get; set; }
        public string? ReciptUrl { get; set; }
        public bool? IsFuel { get; set; }
        public string? Vendor { get; set; }
        public double? Liters { get; set; }
        public double? Cost { get; set; }
        public string? ItemDescription { get; set; }
        public bool? FromMobile { get; set; }
        public bool IsPaid { get; set; }
        public int PaymentStatus { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual User? DriverNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual PaymentStatus PaymentStatusNavigation { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
    }
}
