using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.TaskDTOs;

namespace TruckMove.API.BLL.Services.JobServices
{
    public interface IJobTaskService
    {
        Task<Response<PermitsAndPlateDto>> PermitsAndPlatePostPut(PermitsAndPlateDto permitsAndPlate, int userId);
    }
}
