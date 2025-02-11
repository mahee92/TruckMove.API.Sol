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
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Services.JobServices
{
    public interface IJobService
    {
        Task<Response<JobDto>> PostPutAsync(JobDto job,int userId, string apiKey);
        Task<Response> GetNextJobId();
        Task<Response<JobOutPutDTO>> GetAsync(int id);

        IQueryable<JobOutPutDTO> GetAllAsync();


        Task<Response> ContactAddDelete(int id, List<int> contacts);
        Task<Response<VehicleDto>> VehiclePostPutAsync(VehicleDto vehicle, int userId);
       


        Task<Response<WayPointDto>> WayPointAddDelete(int id,List<WayPointDto> wayPoints);
       
        IQueryable<MobileJobDto> GetAllAsync(int driverId);
        Task<Response<ChecklistDto>> ChecklistPutAsync(ChecklistDto checkList, int v);

        Task<Response<NoteDto>> NotePostPutAsync(NoteDto note, int userId);
        Task<Response> NoteDeleteAsync(int id);
        Task<Response<ImageDto>> ImagePostAsync(ImageDto image, int userId);

        Task<Response> ImageDeleteAsync(int id);
        Task<Response<TrailerDto>> TrailerPostPutAsync(TrailerDto trailer,string apiKey, int userId);
        Task<Response> TrailerDeleteAsync(int id);

        Task<Response<LegDto>> LegPostPutAsync(LegDto leg, string apiKey, int userId);

        Task<Response> ChangeLegPaymentStatus(int legId, PaymentStatusEnum status, int userId);
        Task<Response> IsDriverChangeAllowed(int jobId);

        Task<Response> ReportDelay(int legId, int jobId,string endLocation, string apiKey, int userId);

        Task<Response> ResolveDelay(int jobId, int userId);

        Task<Response<LegHistoryDto>> GetLegHistory(int jobId);
        Task<Response<TrailerOutPutDto>> GetTrailersByJobId(int jobId);

        Task<Response<TrailerOutPutDto>> HookTrailer(int trailerId,int legId);
        Task<Response<TrailerOutPutDto>> DropTrailer(int trailerId);

    }
}
