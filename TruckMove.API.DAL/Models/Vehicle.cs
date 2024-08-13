using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class Vehicle : AuditableEntity, IActiveEntity, IJobUpdatable
    {
        public Vehicle()
        {

            Notes = new HashSet<Note>();
            Images = new HashSet<Image>();
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
        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public bool ShouldUpdateJob => true;
    }
}
