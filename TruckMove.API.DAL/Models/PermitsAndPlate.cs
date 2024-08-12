using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public  class PermitsAndPlate : AuditableEntity, IActiveEntity, IJobUpdatable
    {
        //public bool ShouldUpdateJob => true;
        public PermitsAndPlate()
        {
            Notes = new HashSet<Note>();
            Attachments = new HashSet<Attachment>();
           
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public bool OrganizeNow { get; set; }
                    
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public string? PermitNumber { get; set; }
        public string? PlateNumber { get; set; }
        public double? CostForPermit { get; set; }
        public int JobId { get; set; }
        public bool IsActive { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }

        
    }
}
