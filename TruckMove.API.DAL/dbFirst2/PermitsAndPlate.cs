using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
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
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public string? PermitNumber { get; set; }
        public string? PlateNumber { get; set; }
        public double? CostForPermit { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public int JobId { get; set; }
        public bool? OrganizeNow { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual User? UpdatedBy { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
