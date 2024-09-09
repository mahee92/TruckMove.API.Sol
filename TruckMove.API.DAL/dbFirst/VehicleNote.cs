using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class VehicleNote
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? Note { get; set; }
        public bool? IsVisibleToDriver { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
