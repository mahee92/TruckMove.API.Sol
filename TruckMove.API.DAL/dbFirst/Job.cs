using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Job
    {
        public Job()
        {
            JobContacts = new HashSet<JobContact>();
        }

        public int Id { get; set; }
        public int? Controller { get; set; }
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DropOfLocation { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public int? VehicleId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual User? ControllerNavigation { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual Vehicle? VehicleNavigation { get; set; }
        public virtual ICollection<JobContact> JobContacts { get; set; }
    }
}
