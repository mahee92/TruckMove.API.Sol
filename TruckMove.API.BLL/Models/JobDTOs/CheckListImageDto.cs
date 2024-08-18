using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class CheckListImageDto
    {
        public int Id { get; set; }
        public int ChecklistId { get; set; }
        public string? Url { get; set; }

        
    }
}
