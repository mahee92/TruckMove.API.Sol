using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Purchase : AuditableEntity, IActiveEntity,IJobUpdatable
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public int? Driver { get; set; }
        public bool? FromMobile { get; set; }
        public bool OrganizeNow { get; set; }
        public int? Assignee { get; set; }
        public string? ReciptUrl { get; set; }
        public bool? IsFuel { get; set; }
        public string? Vendor { get; set; }
        public double? Liters { get; set; }
        public double? Cost { get; set; }
        public string? ItemDescription { get; set; }

        public bool IsActive { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual User? DriverNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;

       // public bool ShouldUpdateJob => true; // This is just a property, not a database column       

        
    }
}
