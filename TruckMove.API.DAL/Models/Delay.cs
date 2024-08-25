using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public class Delay : AuditableEntity, IActiveEntity, IJobUpdatable
    { 

        public Delay()
        {
            DelayDrivers = new HashSet<DelayDriver>();
        }

        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public bool? OrganizeNow { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool IsActive { get; set; }

        public virtual User? AssigneeNavigation { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual TaskStatus StatusNavigation { get; set; } = null!;
        public virtual ICollection<DelayDriver> DelayDrivers { get; set; }

        public bool ShouldUpdateJob => true;
    }

}
