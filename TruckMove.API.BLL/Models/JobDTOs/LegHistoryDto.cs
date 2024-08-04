using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.UserManagmentDTO;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class LegHistoryDto
    {

        public int Id { get; set; }
       
        public int DriverId { get; set; }
        public int LegNumber { get; set; }
        public int Status { get; set; }
       
        public virtual LegStatusDto StatusNavigation { get; set; } = null!;
        public virtual UserDto Driver { get; set; } = null!;



    }
}
