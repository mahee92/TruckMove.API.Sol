using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int? VehicleId { get; set; }
        //public int? TrailerId { get; set; }
        //public int? PreDeparturechecklistId { get; set; }
        public bool? VisibletoDriver { get; set; }
        public string? NoteText { get; set; }

    }
}
