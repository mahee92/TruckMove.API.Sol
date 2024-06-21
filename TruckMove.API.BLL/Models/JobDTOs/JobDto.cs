using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class JobDto
    {
        [Key]
        public int Id { get; set; }       
      
        public int? Controller { get; set; }
        [Required]
        public int CompanyId { get; set; }             
        public string? PickupLocation { get; set; }      
        public string? DropOfLocation { get; set; }
        public double? TotalDistance { get; set; }
        public double? TotalDrivingTime { get; set; }
        public double? EstimatedDaysofTravel { get; set; }
        //[DateFormat("yyyy-MM-dd")]
        public DateTime? PickupDate { get; set; }
        public int? Driver { get; set; }
        public string? PickupCoordinates { get; set; }
        public string? DropOfCoordinates { get; set; }



    }
}
