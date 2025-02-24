using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class Image
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? ImageUrl { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual Trailer? Trailer { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
