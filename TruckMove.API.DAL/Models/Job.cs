using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Job : AuditableEntity,IActiveEntity
    {
        public Job()
        {
            JobContacts = new HashSet<JobContact>();
            WayPoints = new HashSet<WayPoint>();
        }

        public int? Controller { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropOfLocation { get; set; }
        public int? VehicleId { get; set; }

        public double? TotalDistance { get; set; }
        public double? TotalDrivingTime { get; set; }
        public double? EstimatedDaysofTravel { get; set; }
        public int? Status { get; set; }
        public DateTime? PickupDate { get; set; }
        public int? Driver { get; set; }
       public string? PickupCoordinates { get; set; }
        public string? DropOfCoordinates { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual User? ControllerNavigation { get; set; }
        public virtual ICollection<JobContact> JobContacts { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual Vehicle? VehicleNavigation { get; set; }
        public virtual User? DriverNavigation { get; set; }

        public virtual ICollection<WayPoint> WayPoints { get; set; }
    }
}
