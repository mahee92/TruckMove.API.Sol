using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class DelayDriverDto
    {
        public int Id { get; set; }
        public int DelayId { get; set; }
        public int DriverId { get; set; }
    }
}
