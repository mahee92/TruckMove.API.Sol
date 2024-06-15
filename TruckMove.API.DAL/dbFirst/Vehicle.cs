﻿using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string? Model { get; set; }
        public string? Rego { get; set; }
        public string? Vin { get; set; }
        public string? Year { get; set; }
        public string? Colour { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
