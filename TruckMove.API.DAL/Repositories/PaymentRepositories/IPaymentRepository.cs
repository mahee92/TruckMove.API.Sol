using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.VMmodels;

namespace TruckMove.API.DAL.Repositories.PaymentRepositories
{
    public interface IPaymentRepository
    {
       
        IQueryable<DriverJobPaymentVM> GetQAPendingList(int contoller);
        public IQueryable<DriverJobPaymentVM> GetPayemntList(int status);



        Task<List<DelayDriver>> GetUnpaidDelaysForDriver(int jobId, int driverId);
        Task<List<Leg>> GetUnpaidLegDataForDriver(int jobId, int driverId);

        Task<List<PublicTransport>> GetUnpaidPublicTransportsDataForDriver(int jobId, int driverId);

        Task<DelayDriver> GetDelayDriverAsync(int entityId);
        Task<DelayDriver> DelayDriverUpdateAsync(DelayDriver entity);
       

        Task ExecuteInTransactionAsync(Func<Task> operations);
        Task<List<T>> UpdateListAsync<T>(List<T> entities) where T : class;
        Task<PaymentAdjustment> AddPaymentAdjustmentAsync(PaymentAdjustment entity);

        Task<List<PaymentAdjustment>> GetUnpaidPaymentAjustments(int jobId, int driverId);

        Task DeletePaymentAdjustmentAsync(int id);

    }
}
