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
        #region Lists
        public IQueryable<DriverJobPaymentVM> GetQAPendingList()
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

        public IQueryable<DriverJobPaymentVM> GetPayemntQADoneList()
        {
            var query = _jobSet
                .Where(j => j.Status == (int)JobStatusEnum.Arrived || j.Status == (int)JobStatusEnum.InStore)  // Filter jobs with specific statuses
                .Where(j =>
                    // Check that all related legs have QADone status
                    (!_legdbSet.Any(l => l.JobId == j.Id && l.PaymentStatus != (int)PaymentStatusEnum.QADone)) &&
                    // Check that all related public transport entries have QADone status
                    (!_publicTransportSet.Any(pt => pt.JobId == j.Id && pt.PaymentStatus != (int)PaymentStatusEnum.QADone)) &&
                    // Check that all related delays have QADone status (join delayDriverSet with delaysSet on DelayId)
                    (!_delayDriverSet.Any(dd => _delaysSet
                        .Where(d => d.JobId == j.Id) // Ensure the delay is related to the current job
                        .Any(d => d.Id == dd.DelayId && dd.PaymentStatus != (int)PaymentStatusEnum.QADone)
                    )) &&
                    // Check that all related purchases have QADone status
                    (!_purchaseSet.Any(p => p.JobId == j.Id && p.PaymentStatus != (int)PaymentStatusEnum.QADone))
                )
                .SelectMany(j =>
                    _legdbSet
                        .Where(l => l.JobId == j.Id)
                        .Select(l => new { l.JobId, l.DriverId, j.PickupLocation, j.DropOfLocation })
                    .Concat(
                        _publicTransportSet
                            .Where(pt => pt.JobId == j.Id)
                            .Select(pt => new { pt.JobId, DriverId = pt.Driver ?? 0, PickupLocation = j.PickupLocation, DropOfLocation = j.DropOfLocation })
                    )
                    .Concat(
                        _delayDriverSet
                            .Join(
                                _delaysSet.Where(d => d.JobId == j.Id),
                                dd => dd.DelayId,
                                d => d.Id,
                                (dd, d) => new { d.JobId, dd.DriverId, j.PickupLocation, j.DropOfLocation }
                            )
                    )
                    .Concat(
                        _purchaseSet
                            .Where(p => p.JobId == j.Id)
                            .Select(p => new { p.JobId, DriverId = p.Driver ?? 0, j.PickupLocation, j.DropOfLocation })
                    )
                )
                .Distinct()
                .Join(
                    _driverSet,  // Join with the driver set to get driver details
                    q => q.DriverId,  // Match on DriverId from the previous anonymous objects
                    d => d.Id,  // Match with the Id of the driver from the _driverSet
                    (q, d) => new DriverJobPaymentVM
                    {
                        JobId = q.JobId,
                        DriverName = d.FirstName + " " + d.LastName,  // Concatenate first and last name for the driver
                        DriverId = q.DriverId,
                        PickupLocation = q.PickupLocation,
                        DropOfLocation = q.DropOfLocation
                    }
                );

            return query;
        }
        #endregion





        #region Details
        public async Task<List<DelayDriver>> GetUnpaidDelaysForDriver(int jobId, int driverId)
        {
            return await _delayDriverSet
       .Where(x => x.DriverId == driverId
                && (x.PaymentStatus == (int)PaymentStatusEnum.QAPending
                    || x.PaymentStatus == (int)PaymentStatusEnum.QADone)
                && x.Delay != null
                && x.Delay.JobId == jobId
                && x.Delay.Status == (int)TaskStatusEnum.Completed)
       .Include(x => x.Delay)
       .ToListAsync();
        }

        public async Task<List<Leg>> GetUnpaidLegDataForDriver(int jobId, int driverId)
        {
            return await _legdbSet
                .Where(l => l.JobId == jobId && l.DriverId == driverId && (l.PaymentStatus == (int)PaymentStatusEnum.QAPending || l.PaymentStatus == (int)PaymentStatusEnum.QADone))
                .Include(l => l.Job) // Include the Job entity
                .Include(l => l.Trailers) // Include the Trailers entity
                .ToListAsync();
        }

        public async Task<List<PublicTransport>> GetUnpaidPublicTransportsDataForDriver(int jobId, int driverId)
        {
            return await _publicTransportSet
                .Where(l => l.JobId == jobId && 
                       l.Driver == driverId &&
                       (l.PaymentStatus == (int)PaymentStatusEnum.QAPending || l.PaymentStatus == (int)PaymentStatusEnum.QADone)
                       && l.Status == (int)TaskStatusEnum.Completed
                       )
                .ToListAsync();
        }

        #endregion 




        public async Task<DelayDriver> GetDelayDriverAsync(int entityId)
        {
            return await _delayDriverSet.FirstAsync(x => x.Id == entityId);
        }
        public async Task<DelayDriver> DelayDriverUpdateAsync(DelayDriver entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }




    }
}
