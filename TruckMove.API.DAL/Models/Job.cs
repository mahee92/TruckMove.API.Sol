using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Job : AuditableEntity,IActiveEntity
    {
        public Job()
        {
            Images = new HashSet<Image>();
            JobContacts = new HashSet<JobContact>();
            WayPoints = new HashSet<WayPoint>();
            Notes = new HashSet<Note>();
            Legs = new HashSet<Leg>();
            Trailers = new HashSet<Trailer>();
            PermitsAndPlates = new HashSet<PermitsAndPlate>();
            Accommodations = new HashSet<Accommodation>();
            Acknowledgements = new HashSet<Acknowledgement>();
            PublicTransports = new HashSet<PublicTransport>();
            Purchases = new HashSet<Purchase>();
            Checklists = new HashSet<Checklist>();
            Delays = new HashSet<Delay>();


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

        public DateTime? EstimatedDeliveryDate { get; set; }

        public string? Correspondence { get; set; }



        public virtual Company Company { get; set; } = null!;
        public virtual User? ControllerNavigation { get; set; }
        public virtual ICollection<JobContact> JobContacts { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual Vehicle? VehicleNavigation { get; set; }
        public virtual User? DriverNavigation { get; set; }
        public virtual ICollection<WayPoint> WayPoints { get; set; }
       
        public virtual JobStatus? StatusNavigation { get; set; }

        public virtual ICollection<Checklist> Checklists { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Leg> Legs { get; set; }

        public virtual ICollection<Trailer> Trailers { get; set; }

        public virtual ICollection<PermitsAndPlate> PermitsAndPlates { get; set; }

        public virtual ICollection<Accommodation> Accommodations { get; set; }

        public virtual ICollection<Acknowledgement> Acknowledgements { get; set; }

        public virtual ICollection<PublicTransport> PublicTransports { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Delay> Delays { get; set; }


        [NotMapped]
        public int QALegCount;


    }
}
