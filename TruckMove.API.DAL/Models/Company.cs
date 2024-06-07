using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Company : AuditableEntity , IActiveEntity
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
        public bool IsActive { get; set; }
      
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
