using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class DelayDriver
    {
        public int Id { get; set; }
        public int DelayId { get; set; }
        public int DriverId { get; set; }
        public int PaymentStatus { get; set; }

        public virtual Delay Delay { get; set; } = null!;
        public virtual User Driver { get; set; } = null!;
        public virtual PaymentStatus PaymentStatusNavigation { get; set; } = null!;
    }
}
