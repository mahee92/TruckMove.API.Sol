using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.VehicleDtos;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class MobileJobDto
    {
        [Key]
        public int Id { get; set; }

        public string? PickupLocation { get; set; }

        public string? DropOfLocation { get; set; }

        public VehicleDto? VehicleNavigation { get; set; }

        public DateTime? PickupDate { get; set; }

        public int? Status { get; set; }

        public  PreDepartureChecklistDto? PreDepartureChecklist { get; set; }
        //public PreDepartureChecklistDto? PreDepartureChecklist { get; set; }


    }
}
