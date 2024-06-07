using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirstModel
{
    public partial class Job
    {
        public int? Controller { get; set; }
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public int Id { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? CreatedById { get; set; }
        public string PickupLocation { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual User? ControllerNavigation { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }
    }
}
