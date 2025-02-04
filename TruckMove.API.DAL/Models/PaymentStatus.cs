using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{ 
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            DelayDrivers = new HashSet<DelayDriver>();
            Legs = new HashSet<Leg>();
            PublicTransports = new HashSet<PublicTransport>();
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<DelayDriver> DelayDrivers { get; set; }
        public virtual ICollection<Leg> Legs { get; set; }
        public virtual ICollection<PublicTransport> PublicTransports { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
