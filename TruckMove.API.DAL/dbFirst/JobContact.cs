using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class JobContact
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
    }
}
