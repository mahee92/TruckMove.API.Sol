using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.PaymentDto;
using TruckMove.API.DAL.VMmodels;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Services.PaymentServices
{
    public interface IPaymentService
    {

        IQueryable<DriverJobPaymentVM> GetQAPendingList();
        IQueryable<DriverJobPaymentVM> GetPayemntQADoneList();

        Task<Response> ChangePaymentStatus(PaymentStatusDTO PaymentStatusDTO, int userId);

        Task<object> GetDetails(int jobId, int driverId);

    }
}
