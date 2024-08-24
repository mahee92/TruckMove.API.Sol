using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class UpdateJobStatusDto
    {
        public int JobId { get; set; }
        public JobStatusEnum Status { get; set; }
    }
}
