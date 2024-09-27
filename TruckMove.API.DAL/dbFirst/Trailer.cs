using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Trailer
    {
        public Trailer()
        {
            Images = new HashSet<Image>();
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public int HookupType { get; set; }
        public int JobId { get; set; }
        public string? HookupLocation { get; set; }
        public string? DropOffLocation { get; set; }
        public string? Rego { get; set; }
        public string? Type { get; set; }
        public string? HookupCoordinate { get; set; }
        public string? DropoffCoordinate { get; set; }
        public int? Status { get; set; }

        public virtual HookupType HookupTypeNavigation { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual TrailerStatus? StatusNavigation { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
