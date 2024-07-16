using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class PublicTransportType
    {
        public PublicTransportType()
        {
            PublicTransports = new HashSet<PublicTransport>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<PublicTransport> PublicTransports { get; set; }
    }
}
