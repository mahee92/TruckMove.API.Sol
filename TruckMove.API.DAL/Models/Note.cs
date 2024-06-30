using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Note : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
        public int? PreDeparturechecklistId { get; set; }
        public bool? VisibletoDriver { get; set; }
        public string? NoteText { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual PreDepartureChecklist? PreDeparturechecklist { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        public bool IsActive { get; set; }

        public virtual Trailer? Trailer { get; set; }
    }
}
