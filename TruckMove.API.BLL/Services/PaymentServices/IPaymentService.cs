using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.DAL.VMmodels;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Services.PaymentServices
{
    public interface IPaymentService
    {

        IQueryable<DriverJobPaymentVM> GetAllUnpaidPaymentsForDrivers();
        Task<object> GetCalculatedLegPaymentsAsync(int jobId, int driverId);


        Task<Response> ChangeLegPaymentStatus(int entityId, PaymentStatusEnum status, int userId, bool isLeg, bool isDelay, bool isPurchase, bool isPublicTransport);




    }
}
