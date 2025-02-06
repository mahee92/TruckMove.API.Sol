using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.DAL.VMmodels;

namespace TruckMove.API.BLL.Services.PaymentServices
{
    public interface IPaymentService
    {
        IQueryable<DriverJobPaymentVM> GetAllUnpaidPaymentsForDrivers();
        Task<object> GetCalculatedLegPaymentsAsync(int jobId, int driverId);
    }
}
