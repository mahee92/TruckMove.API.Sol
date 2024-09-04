using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public  class Rates : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Value { get; set; }

        [NotMapped]
        public bool IsActive { get; set; }


    }
}
