using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class JobStatus
    {
        public JobStatus()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
        public string? DarkColour { get; set; }
        public string? LightColour { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
