using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class Leg
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
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public string? EndLocation { get; set; }
        public DateTime? EndTime { get; set; }
        public string StartLocation { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public double? TotalDistance { get; set; }
        public int Variance { get; set; }
        public bool? IsPaid { get; set; }
        public int PaymentStatus { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual User Driver { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual PaymentStatus PaymentStatusNavigation { get; set; } = null!;
        public virtual LegStatus StatusNavigation { get; set; } = null!;
        public virtual User? UpdatedBy { get; set; }
        public virtual Variance VarianceNavigation { get; set; } = null!;
        public virtual Acknowledgement? Acknowledgement { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
