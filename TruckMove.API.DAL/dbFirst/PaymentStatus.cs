using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class PaymentStatus
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
    }
}
