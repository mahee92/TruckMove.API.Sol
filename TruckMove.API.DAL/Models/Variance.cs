using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class Variance
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public double? Rate { get; set; }
    }
}
