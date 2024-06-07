using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Job : AuditableEntity,IActiveEntity
    {       
        public int? Controller { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
       
        public string PickupLocation { get; set; } = null!;
        public string DropOfLocation { get; set; } = null!;


        public virtual Company Company { get; set; } = null!;
        public virtual User? ControllerNavigation { get; set; }
    
    }
}
