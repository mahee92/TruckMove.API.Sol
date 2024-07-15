using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Models.VehicleDTOs;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class MobileJobDto
    {
        [Key]
        public int Id { get; set; }

        public int? Status { get; set; }

        public string? PickupLocation { get; set; }

        public string? DropOfLocation { get; set; }

        public string? PickupCoordinates { get; set; }
       
        public string? DropOfCoordinates { get; set; }
        
        public DateTime? PickupDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }

        public int? VehicleId { get; set; }

        public double? TotalDistance { get; set; }
        public double? TotalDrivingTime { get; set; }
        public double? EstimatedDaysofTravel { get; set; }


        public VehicleOutputDto? VehicleNavigation { get; set; }
        public ChecklistDto? Checklist { get; set; }

        public virtual List<WayPointDto> WayPoints { get; set; }

        public virtual List<TrailerOutPutDto> Trailers { get; set; }

        public virtual List<LegDto> Legs { get; set; }





    }
}
