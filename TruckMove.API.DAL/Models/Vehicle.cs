using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.dbFirst;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Vehicle : AuditableEntity, IActiveEntity
    {
        public Vehicle()
        {
           
            VehicleImages = new HashSet<VehicleImage>();
            VehicleNotes = new HashSet<VehicleNote>();
            VehicleNotes = new HashSet<VehicleNote>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public bool IsActive { get; set; }
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
        public virtual ICollection<VehicleNote> VehicleNotes { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
