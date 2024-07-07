using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class Acknowledgement
    {
        public int Id { get; set; }
        public int LegId { get; set; }
        public bool Acknowledge { get; set; }

        public virtual Leg Leg { get; set; } = null!;
    }
}
