using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Note
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
        public int? PreDeparturechecklistId { get; set; }
        public bool? VisibletoDriver { get; set; }
        public string? Note1 { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual PreDepartureChecklist? PreDeparturechecklist { get; set; }
        public virtual Trailer? Trailer { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
