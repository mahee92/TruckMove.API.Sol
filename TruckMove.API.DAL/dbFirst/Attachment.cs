using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public int? PermitAndPlateId { get; set; }
        public string? Url { get; set; }

        public virtual PermitsAndPlate? PermitAndPlate { get; set; }
    }
}
