using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class LegStatusDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
    }
}
