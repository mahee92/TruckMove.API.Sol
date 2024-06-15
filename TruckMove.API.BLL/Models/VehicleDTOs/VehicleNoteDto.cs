using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.VehicleDTOs
{
    public class VehicleNoteDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? Note { get; set; }
        public bool? IsVisibleToDriver { get; set; }

       
    }
}
