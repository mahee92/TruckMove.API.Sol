using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Images = new HashSet<Image>();
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string? Model { get; set; }
        public string? Rego { get; set; }
        public string? Vin { get; set; }
        public string? Year { get; set; }
        public string? Colour { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }
        public int JobId { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual Job JobNavigation { get; set; } = null!;
        public virtual User? UpdatedBy { get; set; }
        public virtual Job? Job { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
