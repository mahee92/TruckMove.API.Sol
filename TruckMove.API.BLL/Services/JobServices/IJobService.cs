using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.VehicleDtos;
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
        Task<Response<VehicleDto>> VehiclePostPutAsync(VehicleDto vehicle, int userId);
        Task<Response<VehicleNoteDto>> VehicleNotePostPutAsync(VehicleNoteDto note, int userId);
        Task<Response> VehicleNoteDeleteAsync(int id);
        Task<Response<VehicleImageDto>> VehicleImagePostAsync(VehicleImageDto image, int userId);

        Task<Response> VehicleImageDeleteAsync(int id);
    }
}
