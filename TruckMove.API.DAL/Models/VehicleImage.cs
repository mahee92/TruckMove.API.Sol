using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class VehicleImage : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? Image { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
