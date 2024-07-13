using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            Accommodations = new HashSet<Accommodation>();
            PermitsAndPlates = new HashSet<PermitsAndPlate>();
            PublicTransports = new HashSet<PublicTransport>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Accommodation> Accommodations { get; set; }
        public virtual ICollection<PermitsAndPlate> PermitsAndPlates { get; set; }
        public virtual ICollection<PublicTransport> PublicTransports { get; set; }
    }
}
