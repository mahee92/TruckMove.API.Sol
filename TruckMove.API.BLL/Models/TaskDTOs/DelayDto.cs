using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class DelayDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public int? Assignee { get; set; }
        public bool? OrganizeNow { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual List<DelayDriversDto>? DelayDrivers { get; set; }
    }
}
