using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Jobs = new HashSet<Job>();
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

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
        public virtual ICollection<VehicleNote> VehicleNotes { get; set; }
    }
}
