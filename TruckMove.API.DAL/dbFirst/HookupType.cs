using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class HookupType
    {
        public HookupType()
        {
            Trailers = new HashSet<Trailer>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
