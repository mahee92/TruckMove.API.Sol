using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Models
{
    public  class HookupType
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
