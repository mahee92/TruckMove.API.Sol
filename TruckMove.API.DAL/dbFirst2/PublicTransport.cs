using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class PublicTransport
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
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public string? RequiredFromsuburb { get; set; }
        public string? Name { get; set; }
        public bool? IsPaid { get; set; }
        public int PaymentStatus { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual User? DriverNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual PaymentStatus PaymentStatusNavigation { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual PublicTransportType? TransportTypeNavigation { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
