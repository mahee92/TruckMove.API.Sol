using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class VehicleImage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? Image { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
