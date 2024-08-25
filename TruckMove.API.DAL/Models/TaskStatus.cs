using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            PermitsAndPlates = new HashSet<PermitsAndPlate>();
            Accommodations = new HashSet<Accommodation>();
            PublicTransports = new HashSet<PublicTransport>();
            Purchases = new HashSet<Purchase>();
            Delays = new HashSet<Delay>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<PermitsAndPlate> PermitsAndPlates { get; set; }
        public virtual ICollection<Accommodation> Accommodations { get; set; }

        public virtual ICollection<PublicTransport> PublicTransports { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Delay> Delays { get; set; }
    }
}
