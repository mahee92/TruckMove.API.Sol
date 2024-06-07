using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            Contacts = new HashSet<Contact>();
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string CompanyAddress { get; set; } = null!;
        public string PrimaryEmail { get; set; } = null!;
        public string AccountsEmail { get; set; } = null!;
        public string CompanyStreetAddress { get; set; } = null!;
        public string CompanyAbn { get; set; } = null!;
        public string? Logo { get; set; }
        public bool? IsActive { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? CreatedById { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
