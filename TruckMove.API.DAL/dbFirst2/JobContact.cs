using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class JobContact
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ContactId { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual User? CreatedBy { get; set; }
        public virtual Job Job { get; set; } = null!;
        public virtual User? UpdatedBy { get; set; }
    }
}
