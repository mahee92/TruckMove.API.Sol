using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class DriverJobStatus
    {
        public bool HasCurrentJob { get; set; } 
        public int JobId { get; set; }

    }
}
