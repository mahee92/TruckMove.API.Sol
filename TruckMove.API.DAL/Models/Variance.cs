using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class Variance
    {
        public Variance()
        {
            Legs = new HashSet<Leg>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double? Rate { get; set; }

        public virtual ICollection<Leg> Legs { get; set; }
    }
}
