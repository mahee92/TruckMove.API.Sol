using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Contact : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public string ContactName { get; set; } = null!;
        public string? ContactMobileNumber { get; set; }
        public string? ContactLandline { get; set; }
        public string? ContactsEmail { get; set; }
        public string? ContactStreetAddress { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public string? Image { get; set; }
      

        public virtual Company Company { get; set; } = null!;
  
    }
}
