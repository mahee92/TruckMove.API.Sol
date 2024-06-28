using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Leg
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int DriverId { get; set; }
        public int LegNumber { get; set; }
        public int Status { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual LegStatus StatusNavigation { get; set; } = null!;
        public virtual Acknowledgement? Acknowledgement { get; set; }
    }
}
