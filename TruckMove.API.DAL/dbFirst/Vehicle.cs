using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Notes = new HashSet<Note>();
            VehicleImages = new HashSet<VehicleImage>();
            VehicleNotes = new HashSet<VehicleNote>();
        }

        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string? Model { get; set; }
        public string? Rego { get; set; }
        public string? Vin { get; set; }
        public string? Year { get; set; }
        public string? Colour { get; set; }
        public int JobId { get; set; }

        public virtual Job JobNavigation { get; set; } = null!;
        public virtual Job? Job { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
        public virtual ICollection<VehicleNote> VehicleNotes { get; set; }
    }
}
