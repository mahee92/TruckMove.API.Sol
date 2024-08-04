using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class TaskStatusDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
    }
}
