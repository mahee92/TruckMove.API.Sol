using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public  class PermitsAndPlate : AuditableEntity, IActiveEntity
    {
        public PermitsAndPlate()
        {
           // ImagesAndAttachments = new HashSet<ImagesAndAttachment>();
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

        public bool IsActive { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
       // public virtual ICollection<ImagesAndAttachment> ImagesAndAttachments { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
