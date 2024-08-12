using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class PublicTransport : AuditableEntity, IActiveEntity, IJobUpdatable
    {
        public PublicTransport()
        {
            Attachments = new HashSet<Attachment>();
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public int JobId { get; set; }
        public int? Driver { get; set; }
        public bool OrganizeNow { get; set; }
        public int Status { get; set; }
        public DateTime? Daterequired { get; set; }
        public string? RequiredTosuburb { get; set; }
        public int? Assignee { get; set; }
        public string? BookingInstructions { get; set; }
        public int? TransportType { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public string? DepartureAddress { get; set; }
        public string? ArrivalAddress { get; set; }
        public string? ReferenceNumber { get; set; }
        public double? TransportCost { get; set; }
        public string? RequiredFromsuburb { get; set; }

        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual User? DriverNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual PublicTransportType? TransportTypeNavigation { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        //public bool ShouldUpdateJob => true;
    }
}
