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

        IQueryable<DriverJobPaymentVM> GetQAPendingList(int controller);
        IQueryable<DriverJobPaymentVM> GetPayemntList(int status, int controller);
        Task<object> GetDetails(int jobId, int driverId);

        Task<Response> ChangePaymentStatus(PaymentStatusDTO PaymentStatusDTO, int userId);
        Task<Response> VerifyOrPayPayment(int jobId, int driverId, int userId, int status);
       Task<Response> AddPaymentAjustments(PaymentAdjustmentDTO paymentAdjustment, int userId);

        Task<Response> DeletePaymentAjustment(int id);
    }
}
