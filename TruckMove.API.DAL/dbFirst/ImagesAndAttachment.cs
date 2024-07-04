using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class ImagesAndAttachment
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? ImageUrl { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
        public int? ParmitsAndPlatesId { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual PermitsAndPlate? ParmitsAndPlates { get; set; }
        public virtual Trailer? Trailer { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
