using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Attachment : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int? PermitAndPlateId { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; }

        public virtual PermitsAndPlate? PermitAndPlate { get; set; }
    }
}
