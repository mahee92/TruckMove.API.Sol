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
       
        public IQueryable<DriverJobPaymentVM> GetQAPendingList();
       
        Task<List<DelayDriver>> GetUnpaidDelaysForDriver(int jobId, int driverId);
        Task<List<Leg>> GetUnpaidLegDataForDriver(int jobId, int driverId);

        Task<List<PublicTransport>> GetUnpaidPublicTransportsDataForDriver(int jobId, int driverId);

        Task<DelayDriver> GetDelayDriverAsync(int entityId);
        Task<DelayDriver> DelayDriverUpdateAsync(DelayDriver entity);
    }
}
