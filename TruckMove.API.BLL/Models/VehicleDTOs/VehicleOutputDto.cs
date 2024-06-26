using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.VehicleDTOs
{
    public class VehicleOutputDto : VehicleDto
    {
        public virtual List<NoteDto> Notes { get; set; }

        public virtual List<VehicleImageDto> VehicleImages { get; set; }
    }
}
