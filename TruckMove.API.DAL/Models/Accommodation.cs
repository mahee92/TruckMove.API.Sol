using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Accommodation : AuditableEntity, IActiveEntity
    {
        public Accommodation()
        {
            Notes = new HashSet<Note>();
            
        }

        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? Driver { get; set; }
        public string? Location { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ReferenceNumber { get; set; }
        public double? Price { get; set; }
        public bool IsActive { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual User? DriverNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual ICollection<Note> Notes { get; set; }
        
    }
}
