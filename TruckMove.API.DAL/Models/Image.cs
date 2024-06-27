using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Image : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? ImageUrl { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }

        public bool IsActive { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual Vehicle? Vehicle { get; set; }
    }
}
