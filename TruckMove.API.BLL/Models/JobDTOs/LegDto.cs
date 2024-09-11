using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class LegDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }

        public int LegNumber { get; set; }
        public string StartLocation { get; set; } = null!;
        public string? EndLocation { get; set; }
        public int? Status { get; set; }

        public bool Acknowledged { get; set; }

        
        //public bool IsCompleted { get; set; } /*arrived to destination*/

        //public bool? InStore { get; set; }

        //public bool? Delay { get; set; }

        public int JobStatus { get; set; }


    }
}
