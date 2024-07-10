using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            PermitsAndPlates = new HashSet<PermitsAndPlate>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<PermitsAndPlate> PermitsAndPlates { get; set; }
    }
}
