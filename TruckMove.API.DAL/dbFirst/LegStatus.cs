using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class LegStatus
    {
        public LegStatus()
        {
            Legs = new HashSet<Leg>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Leg> Legs { get; set; }
    }
}
