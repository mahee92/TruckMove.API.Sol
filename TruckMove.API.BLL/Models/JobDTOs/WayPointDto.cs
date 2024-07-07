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
    public partial class WayPointDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? Location { get; set; }
        public string? Coordinates { get; set; }

       
    }
}
