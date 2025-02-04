using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Leg : AuditableEntity, IActiveEntity, IJobUpdatable
    {
        public Leg()
        {
            Trailers = new HashSet<Trailer>();
        }

        public int Id { get; set; }
        public int JobId { get; set; }
        public int DriverId { get; set; }
        public int LegNumber { get; set; }
        public int Status { get; set; }
        public int Variance { get; set; }
        public string StartLocation { get; set; } = null!;
   
        public string? EndLocation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? TotalDistance { get; set; }
        public bool IsActive { get; set; }

        public bool IsPaid { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual LegStatus StatusNavigation { get; set; } = null!;
        public virtual Variance VarianceNavigation { get; set; } = null!;
        public virtual Acknowledgement? Acknowledgement { get; set; }

        public virtual User Driver { get; set; } = null!;

        public bool ShouldUpdateJob => true;

        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
