using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.DAL.VMmodels
{
    public class DriverJobPaymentVM
    {
        
        public string? DriverName { get; set; }
        public int DriverId { get; set; }

        public string? PickupLocation { get; set; }
        public string? DropOfLocation { get; set; }

        public int JobId { get; set; }
    }
}
