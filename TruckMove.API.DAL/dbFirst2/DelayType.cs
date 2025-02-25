using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class DelayType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
    }
}
