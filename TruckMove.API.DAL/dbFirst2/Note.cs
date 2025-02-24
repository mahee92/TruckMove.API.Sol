using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class Note
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
        public bool? VisibletoDriver { get; set; }
        public string? NoteText { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public int? PermitAndPlatesId { get; set; }
        public int? AccommodationId { get; set; }
        public int? PublicTransportId { get; set; }
        public int? ChecklistId { get; set; }
        public int? DelayId { get; set; }

        public virtual Accommodation? Accommodation { get; set; }
        public virtual Checklist? Checklist { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual Delay? Delay { get; set; }
        public virtual Job? Job { get; set; }
        public virtual PermitsAndPlate? PermitAndPlates { get; set; }
        public virtual PublicTransport? PublicTransport { get; set; }
        public virtual Trailer? Trailer { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
