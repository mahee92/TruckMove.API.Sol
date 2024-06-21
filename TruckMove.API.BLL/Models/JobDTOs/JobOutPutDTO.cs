using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Models.VehicleDTOs;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class JobOutPutDTO : JobDto
    {
        public virtual List<ContactDto> Contacts { get; set; }

        public virtual CompanyDto Company { get; set; }

        public virtual VehicleOutputDto Vehicle { get; set; }

        public virtual List<WayPointDto> WayPoints { get; set; }

    }
}
