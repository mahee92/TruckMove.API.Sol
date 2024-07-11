using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class PermitsAndPlate
    {
        public PermitsAndPlate()
        {
            Attachments = new HashSet<Attachment>();
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public bool OrganiseNow { get; set; }
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public string? PermitNumber { get; set; }
        public string? PlateNumber { get; set; }
        public double? CostForPermit { get; set; }
        public int JobId { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
