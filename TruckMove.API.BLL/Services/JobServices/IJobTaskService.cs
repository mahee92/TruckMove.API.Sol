using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.TaskDTOs;

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
        Task<Response<PublicTransportDto>> PublicTransportPostPut(PublicTransportDto transport, int userId);

        Task<Response> PublicTransportDeleteAsync(int id);
    }
}
