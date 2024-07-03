using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class Variance
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double? Rate { get; set; }
    }
}
