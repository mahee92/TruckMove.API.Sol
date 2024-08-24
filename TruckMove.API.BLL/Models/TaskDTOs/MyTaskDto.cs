using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Models.VehicleDTOs;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class MyTaskDto
    {
       
        public virtual List<PermitsAndPlateOutputDto>? PermitsAndPlates { get; set; }
        public virtual List<AccommodationOutputDto>? Accommodations { get; set; }

        public virtual List<PublicTransportOutputDto>? PublicTransports { get; set; }

        public virtual List<PurchaseOutputDto>? Purchases { get; set; }

        public virtual List<Job>? DrivingTasks { get; set; }
        //





    }
}
