using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class TrailerStatus
    {
        public TrailerStatus()
        {
            Trailers = new HashSet<Trailer>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
