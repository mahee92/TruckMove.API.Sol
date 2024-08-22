using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.dbFirst;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Note : AuditableEntity, IActiveEntity, IJobUpdatable
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
       
        public bool? VisibletoDriver { get; set; }
        public string? NoteText { get; set; }
        public int? PermitAndPlatesId { get; set; }

        public bool IsActive { get; set; }
        public int? AccommodationId { get; set; }

        public int? PublicTransportId { get; set; }

        public int? ChecklistId { get; set; }

        public virtual Accommodation? Accommodation { get; set; }
        public virtual Job? Job { get; set; } = null!;
        public virtual PermitsAndPlate? PermitAndPlates { get; set; }
        public virtual Trailer? Trailer { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual PublicTransport? PublicTransport { get; set; }

        public virtual Checklist? Checklist { get; set; }

        public bool ShouldUpdateJob => true;
        
        [NotMapped]
        int IJobUpdatable.JobId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
