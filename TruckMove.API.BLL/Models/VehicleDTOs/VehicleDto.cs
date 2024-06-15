using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.VehicleDTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Rego { get; set; }
        public string? Vin { get; set; }
        public string? Year { get; set; }
        public string? Colour { get; set; }
      
    }
}
