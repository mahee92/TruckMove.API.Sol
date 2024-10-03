using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class TrailerStatusDto
    {
        public int? Id { get; set; }
        public string? Status { get; set; } = null!;
    }
}
