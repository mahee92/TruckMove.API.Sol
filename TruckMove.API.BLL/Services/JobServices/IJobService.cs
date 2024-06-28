using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Models.VehicleDTOs;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Services.JobServices
{
    public interface IJobService
    {
        Task<Response<JobDto>> PostPutAsync(JobDto job,int userId);
        Task<Response> GetNextJobId();
        Task<Response<JobOutPutDTO>> GetAsync(int id);
       
        Task<Response> ContactAddDelete(int id, List<int> contacts);
        Task<Response<VehicleDto>> VehiclePostPutAsync(VehicleDto vehicle, int userId);
       


        Task<Response<WayPointDto>> WayPointAddDelete(List<WayPointDto> wayPoints);
        Response<DriverJobStatus> GetDriverJobStaus(int driverId);
        IQueryable<MobileJobDto> GetAllAsync(int driverId);
        Task<Response<PreDepartureChecklistDto>> PreDepartureChecklistPutAsync(PreDepartureChecklistDto checkList, int v);

        Task<Response<NoteDto>> NotePostPutAsync(NoteDto note, int userId);
        Task<Response> NoteDeleteAsync(int id);
        Task<Response<ImageDto>> ImagePostAsync(ImageDto image, int userId);

        Task<Response> ImageDeleteAsync(int id);



    }
}
