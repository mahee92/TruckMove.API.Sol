using Microsoft.EntityFrameworkCore;
using System.Linq;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.VMmodels;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.DAL.Repositories.PaymentRepositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Leg> _legdbSet;
        private readonly DbSet<Purchase> _purchaseSet;
        private readonly DbSet<PublicTransport> _publicTransportSet;
        private readonly DbSet<DelayDriver> _delayDriverSet;
        private readonly DbSet<Delay> _delaysSet;
        private readonly DbSet<User> _driverSet;
        private readonly DbSet<Job> _jobSet;

        public PaymentRepository(DbContextOptions<TrukMoveContext> options)
        {

            _context = new TrukMoveContext(options);
            _legdbSet = _context.Set<Leg>();
            _purchaseSet = _context.Set<Purchase>();
            _publicTransportSet = _context.Set<PublicTransport>();
            _delayDriverSet = _context.Set<DelayDriver>();

            _delaysSet = _context.Set<Delay>();
            _driverSet = _context.Set<User>();
            _jobSet = _context.Set<Job>();

        }


      
        public IQueryable<DriverJobPaymentVM> GetAllUnpaidPaymentsForDrivers()
        {
            var query = _jobSet
               .Where(j => j.Status == (int)JobStatusEnum.Arrived || j.Status == (int)JobStatusEnum.InStore)
               .SelectMany(j => _legdbSet
                   .Where(l => l.PaymentStatus == (int)PaymentStatusEnum.QAPending && l.JobId == j.Id)
                   .Select(l => new { l.JobId, l.DriverId, j.PickupLocation, j.DropOfLocation })
               .Concat(
                   _purchaseSet
                   .Where(p => p.PaymentStatus == (int)PaymentStatusEnum.QAPending && p.Driver != null && p.JobId == j.Id && p.Status == (int)TaskStatusEnum.Completed)
                   .Select(p => new { p.JobId, DriverId = p.Driver ?? 0, j.PickupLocation, j.DropOfLocation })
               )
               .Concat(
                   _publicTransportSet
                   .Where(pt => pt.PaymentStatus == (int)PaymentStatusEnum.QAPending && pt.Driver != null && pt.JobId == j.Id && pt.Status == (int)TaskStatusEnum.Completed)
                   .Select(pt => new { pt.JobId, DriverId = pt.Driver ?? 0, j.PickupLocation, j.DropOfLocation })
               )
               .Concat(
                   _delayDriverSet
                   .Where(dd => dd.PaymentStatus == (int)PaymentStatusEnum.QAPending)
                   .Join(
                       _delaysSet.Where(d => d.JobId == j.Id && d.Status == (int)TaskStatusEnum.Completed),
                       dd => dd.DelayId,
                       d => d.Id,
                       (dd, d) => new { d.JobId, dd.DriverId, j.PickupLocation, j.DropOfLocation }
                   )
               )
               )
               .Distinct()
               .Join(
                   _driverSet,
                   q => q.DriverId,
                   d => d.Id,
                   (q, d) => new DriverJobPaymentVM
                   {
                       JobId = q.JobId,
                       DriverName = d.FirstName + " " + d.LastName,
                       DriverId = q.DriverId,
                       PickupLocation = q.PickupLocation,
                       DropOfLocation = q.DropOfLocation
                   }
               );

            return query;
        }



        public async Task<List<Leg>> GetUnpaidLegDataForDriver(int jobId, int driverId)
        {
            return await _legdbSet
                .Where(l => l.JobId == jobId && l.DriverId == driverId && l.PaymentStatus == (int)PaymentStatusEnum.QAPending)
                .Include(l => l.Job) // Include the Job entity
                .ToListAsync();
        }



    }
}
