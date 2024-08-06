using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class PermitsAndPlateDto
    {
        public int Id { get; set; }

         public int JobId { get; set; }
        public string Type { get; set; } = null!;
        public bool? OrganizeNow { get; set; }
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public string? PermitNumber { get; set; }
        public string? PlateNumber { get; set; }
        public double? CostForPermit { get; set; }

       


    }
}
