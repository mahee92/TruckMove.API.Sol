using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class DelayType
    {
        public DelayType()
        {
            Delays = new HashSet<Delay>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Delay> Delays { get; set; }
    }
}
