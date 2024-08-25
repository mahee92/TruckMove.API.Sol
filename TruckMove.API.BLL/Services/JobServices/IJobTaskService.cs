using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.TaskDTOs;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Services.JobServices
{
    public interface IJobTaskService
    {
        Task<Response<PermitsAndPlateDto>> PermitsAndPlatePostPut(PermitsAndPlateDto permitsAndPlate, int userId);
        Task<Response> PermitsAndPlateDeleteAsync(int id);
        Task<Response<AttachmentDto>> AttachmentPostAsync(AttachmentDto attachment, int userId);
        Task<Response> ImageDeleteAsync(int id);
        Task<Response<AccommodationDto>> AccommodationPostPut(AccommodationDto accommodation, int userId);
        Task<Response> AccommodationDeleteAsync(int id);
        Task<Response<PublicTransportOutputDto>> PublicTransportPostPut(PublicTransportDto transport, int userId);

        Task<Response> PublicTransportDeleteAsync(int id);
        Task<Response<PurchaseOutputDto>> PurchasePostPut(PurchaseDto purchase, int v);
        Task<Response> PurchaseDeleteAsync(int id);
        Task<bool> IsDriverValidForJob(int jobId, int driverId);

        Task<Response<MyTaskDto>> GetMyTasks(int userId);

        Task<Response<GraphData>> GetGraphData();
    }
}
