using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Leg : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int DriverId { get; set; }
        public int LegNumber { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual LegStatus StatusNavigation { get; set; } = null!;
    }
}
