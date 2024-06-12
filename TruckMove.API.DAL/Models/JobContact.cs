using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class JobContact : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ContactId { get; set; }

        public bool IsActive { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;

       
    }
}
