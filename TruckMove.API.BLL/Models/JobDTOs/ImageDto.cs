using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? ImageUrl { get; set; }
        public int? VehicleId { get; set; }
        public int? TrailerId { get; set; }
    }
}
