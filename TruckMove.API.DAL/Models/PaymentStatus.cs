using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{ 
    public partial class PaymentStatus
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
    }
}
