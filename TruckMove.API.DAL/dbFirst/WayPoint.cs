using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class WayPoint
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? Location { get; set; }
        public string? Coordinates { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
