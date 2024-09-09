using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Contact
    {
        public Contact()
        {
            JobContacts = new HashSet<JobContact>();
        }

        public int Id { get; set; }
        public string ContactName { get; set; } = null!;
        public string? ContactMobileNumber { get; set; }
        public string? ContactLandline { get; set; }
        public string? ContactsEmail { get; set; }
        public string? ContactStreetAddress { get; set; }
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual User? CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual ICollection<JobContact> JobContacts { get; set; }
    }
}
