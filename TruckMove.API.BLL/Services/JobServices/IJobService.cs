using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.VehicleDTOs;

namespace TruckMove.API.BLL.Services.JobServices
{
    public interface IJobService
    {
        Task<Response<JobDto>> PostPutAsync(JobDto job,int userId);
        Task<Response> GetNextJobId();
        Task<Response<JobOutPutDTO>> GetAsync(int id);
        Task<Response<JobDto>> GetAllAsync();
        Task<Response> ContactAddDelete(int id, List<int> contacts);
        Task<Response<VehicleDTO>> VehiclePostPutAsync(VehicleDTO vehicle, int v);
    }
}
