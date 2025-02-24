using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public int? PermitAndPlateId { get; set; }
        public string? Url { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public int? AccommodationId { get; set; }
        public int? PublicTransportId { get; set; }

        public virtual Accommodation? Accommodation { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual PermitsAndPlate? PermitAndPlate { get; set; }
        public virtual PublicTransport? PublicTransport { get; set; }
        public virtual User? UpdatedBy { get; set; }
    }
}
