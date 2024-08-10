using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Attachment : AuditableEntity, IActiveEntity, IJobUpdatable
    {

        public int Id { get; set; }
        public int? PermitAndPlateId { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; }
        public int? AccommodationId { get; set; }

        public int? PublicTransportId { get; set; }

        public virtual PermitsAndPlate? PermitAndPlate { get; set; }

        public virtual Accommodation? Accommodation { get; set; }

        public virtual PublicTransport? PublicTransport { get; set; }


        public bool ShouldUpdateJob => true;

        [NotMapped]
        public int JobId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}
