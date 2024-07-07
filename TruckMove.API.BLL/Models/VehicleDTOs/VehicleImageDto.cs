using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.VehicleDTOs
{
    public class VehicleImageDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? Image { get; set; }
    }
}
