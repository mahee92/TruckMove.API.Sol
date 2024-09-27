using Microsoft.EntityFrameworkCore;
using TruckMove.API.DAL.Models;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContext _context;
        private readonly DbContextOptions<TrukMoveContext> _options;
        public TaskRepository(DbContextOptions<TrukMoveContext> options)
        {

            _context = new TrukMoveContext(options);
            _options = options;

        }

        public async Task<List<Accommodation>> GetAccommodationTasksByUserId(int userId)
        {
            
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<Accommodation>().Where(x => x.Job.IsActive == true &&
                                                                  //x.Job.Status < (int)JobStatusEnum.QADone &&-- should be accomodation QA done
                                                                  x.OrganizeNow == false &&
                                                                  x.Assignee == userId &&
                                                                  x.Status != (int)TaskStatusEnum.Completed &&
                                                                  x.IsActive == true)
                                                            .Select(x => new Accommodation
                                                            {
                                                                JobId = x.JobId,
                                                                Id = x.Id,
                                                                DriverNavigation = x.DriverNavigation,
                                                                BookingDate = x.BookingDate,
                                                            }).ToListAsync();
            }

        }
        public async Task<List<PermitsAndPlate>> GetPermitsAndPlateTasksByUserId(int userId)
        {
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<PermitsAndPlate>().Where(x => x.Job.IsActive == true &&
                                                                   //x.Job.Status < (int)JobStatusEnum.QADone &&
                                                                   x.OrganizeNow == false &&
                                                                   x.Assignee == userId &&
                                                                   x.Status != (int)TaskStatusEnum.Completed &&
                                                                   x.IsActive == true).Select(x => new PermitsAndPlate
                                                                   {
                                                                       JobId = x.JobId,
                                                                       Id = x.Id,
                                                                       Type = x.Type
                                                                   }).ToListAsync();
            }
        }
        public async Task<List<PublicTransport>> GetPublicTransportTasksByUserId(int userId)
        {
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<PublicTransport>().Where(x => x.Job.IsActive == true &&
                                                                   //x.Job.Status < (int)JobStatusEnum.QADone &&
                                                                   x.OrganizeNow == false &&
                                                                   x.Assignee == userId &&
                                                                   x.Status != (int)TaskStatusEnum.Completed &&
                                                                   x.IsActive == true).Select(x => new PublicTransport
                                                                   {
                                                                       JobId = x.JobId,
                                                                       Id = x.Id,
                                                                       DriverNavigation = x.DriverNavigation,
                                                                       Daterequired = x.Daterequired,
                                                                   }).ToListAsync();
            }
        }
        public async Task<List<Purchase>> GetPurchaseTasksByUserId(int userId)
        {
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<Purchase>().Where(x => x.Job.IsActive == true &&
                                                                   //x.Job.Status < (int)JobStatusEnum.QADone &&
                                                                   x.OrganizeNow == false &&
                                                                   x.Assignee == userId &&
                                                                   x.Status != (int)TaskStatusEnum.Completed &&
                                                                   x.IsActive == true).Select(x => new Purchase
                                                                   {
                                                                       JobId = x.JobId,
                                                                       Id = x.Id,
                                                                       DriverNavigation = x.DriverNavigation
                                                                   }).ToListAsync();
            }
        }

        public async Task<List<Delay>> GetDelayTasksByUserId(int userId)
        {
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<Delay>().Where(x => x.Job.IsActive == true &&
                                                                   //x.Job.Status < (int)JobStatusEnum.QADone &&
                                                                   x.OrganizeNow == false &&
                                                                   x.Assignee == userId &&
                                                                   x.Status != (int)TaskStatusEnum.Completed &&
                                                                   x.IsActive == true).Select(x => new Delay
                                                                   {
                                                                       JobId = x.JobId,
                                                                       Id = x.Id                                                       
                                                                   }).ToListAsync();
            }
        }

        public async Task<List<Job>> GetDrivingTasksByUserId(int userId)
        {
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<Job>().Where(x => x.IsActive == true &&
                                                                   x.Status < (int)JobStatusEnum.PreDepartureChecked &&
                                                                   x.Driver == userId
                                                                   ).Select(x => new Job
                                                                   {
                                                                       Id = x.Id,
                                                                   }).ToListAsync();
            }
        }
        public async Task<List<Job>> GetJobsByUserId(int userId)
        {
            using (var context = new TrukMoveContext(_options))
            {
                return await context.Set<Job>()
                                     .Where(x => x.IsActive == true &&
                                                 x.Controller == userId &&
                                                 x.Status != (int)JobStatusEnum.Completed)
                                     .Include(x => x.Vehicle) // Eagerly load navigation properties
                                     .Include(x => x.StatusNavigation)
                                     .Include(x => x.DriverNavigation)
                                     // Uncomment if you need the count logic
                                     //.Select(x => new Job
                                     //{
                                     //    Id = x.Id,
                                     //    PickupLocation = x.PickupLocation,
                                     //    DropOfLocation = x.DropOfLocation,
                                     //    QALegCount = x.Legs.Count(y => y.Status == (int)TaskStatusEnum.Completed),
                                     //})
                                     .OrderByDescending(x => x.LastModifiedDate)
                                     .ToListAsync();
            }
        }

        public async Task<Dictionary<DateTime, int>> GetUpcommingJobsByPickupDate()
        {
            using (var context = new TrukMoveContext(_options))
            {
                // Fetch the relevant data from the database first
                var upcomingJobDates = await context.Set<Job>()
                    .Where(job => job.IsActive &&
                           job.PickupDate.HasValue &&
                           job.PickupDate != null
                          && job.PickupDate.Value > DateTime.Now)
                    .Select(job => job.PickupDate.Value.Date)
                    .ToListAsync();

                // Group by PickupDate and count the number of jobs for each date
                var groupedJobVolumes = upcomingJobDates
                    .GroupBy(date => date)
                    .OrderBy(group => group.Key)
                    .ToDictionary(group => group.Key, group => group.Count());

                return groupedJobVolumes;
            }
        }

        public async Task<Dictionary<string, int>> GetUpcomingJobsByCompany()
        {
            using (var context = new TrukMoveContext(_options))
            {
                // Fetch the relevant data from the database first
                var upcomingJobs = await context.Set<Job>()
                    .Where(job => job.IsActive &&
                           job.PickupDate.HasValue &&
                            job.PickupDate != null
                           && job.PickupDate.Value > DateTime.Now)
                    .Select(job => new { job.CompanyId, job.Company.CompanyName })
                    .ToListAsync();

                // Group by CompanyId and include CompanyName
                var groupedJobVolumes = upcomingJobs
                    .GroupBy(job => new { job.CompanyId, job.CompanyName })
                    .OrderBy(group => group.Key.CompanyName)
                    .ToDictionary(
                        group => group.Key.CompanyName, // Use CompanyName as the key
                        group => group.Count()           // Use the count of jobs as the value
                    );

                return groupedJobVolumes;
            }
        }
        public async Task<Dictionary<string, int>> GetUpcomingJobsByDriver()
        {
            using (var context = new TrukMoveContext(_options))
            {
                // Fetch the relevant data from the database first
                var upcomingJobs = await context.Set<Job>()
                    .Where(job => job.IsActive &&
                           job.PickupDate.HasValue &&
                           job.PickupDate.Value > DateTime.Now &&
                           job.DriverNavigation !=null)
                    .Select(job => new { job.Driver, job.DriverNavigation.FirstName, job.DriverNavigation.LastName })
                    .ToListAsync();

                // Group by DriverId and include Driver's Full Name
                var groupedJobVolumes = upcomingJobs
                    .GroupBy(job => new { job.Driver, FullName = job.FirstName + " " + job.LastName })
                    .OrderBy(group => group.Key.FullName)
                    .ToDictionary(
                        group => group.Key.FullName, // Use FullName as the key
                        group => group.Count()       // Use the count of jobs as the value
                    );

                return groupedJobVolumes;
            }
        }
        public async Task<Dictionary<string, int>> GetUpcomingJobsByDriverOverTime()
        {
            using (var context = new TrukMoveContext(_options))
            {
                var upcomingJobs = await context.Set<Job>()
                    .Where(job => job.IsActive &&
                                  job.PickupDate.HasValue &&
                                  job.PickupDate != null &&
                                  job.DriverNavigation != null && 
                                  job.PickupDate.Value > DateTime.Now )
                    .Select(job => new
                    {
                        job.DriverNavigation.FirstName,
                        job.DriverNavigation.LastName,
                        Date = job.PickupDate.Value.Date
                    })
                    .ToListAsync();

                // Group by Driver (FirstName + LastName) and Date, then count the number of jobs for each group
                var groupedJobVolumes = upcomingJobs
                    .GroupBy(job => (DriverName: $"{job.FirstName} {job.LastName}", job.Date))
                    .OrderBy(group => group.Key.Date)
                    .ThenBy(group => group.Key.DriverName)
                    .ToDictionary(
                        group => $"{group.Key.DriverName}_{group.Key.Date:yyyy-MM-dd}", // Convert tuple to string key
                        group => group.Count());

                return groupedJobVolumes;
            }
        }

        public async Task<List<int>> GetJobsEligibleForPaymentQA(int userId)
        {
            var filteredJobs = await _context.Set<Job>()
                                         .Include(j => j.Purchases) // Include related Purchases
                                         .Include(j => j.Delays)    // Include related Delays
                                         .Where(j => j.Status == (int)JobStatusEnum.ArrivalChecked &&
                                                     j.Controller == userId &&
                                                     j.IsActive == true)
                                         .ToListAsync();

            var res = filteredJobs
                                              .Where(j =>
                                                  // All delays must be completed and unpaid, if there are any delays
                                                  (!j.Delays.Any() || j.Delays.All(d => d.Status == (int)TaskStatusEnum.Completed /*&& d.IsPaid == false && .QAdone == false*/)) &&

                                                  // All purchases must be completed and unpaid, if there are any purchases
                                                  (!j.Purchases.Any() ||j.Purchases.All(p => p.Status == (int)TaskStatusEnum.Completed /*&& d.IsPaid == false */)

                                                 // At least one purchase should not be QA verified
                                                 ////&& j.Purchases.Any(p => p.QADone == false)
                                              ))
                                              .Select(j => j.Id)
                                              .ToList();

             return res;
        }
        public async Task<List<int>> GetJobsEligiblePayment(int userId)
        {
            // Apply initial restrictive filters first
            var filteredJobs = await _context.Set<Job>()
                                             .Where(j => j.Status == (int)JobStatusEnum.ArrivalChecked /*|| p.Instore == false*/ &&
                                                         j.Controller == userId &&
                                                         j.IsActive == true)
                                             .ToListAsync();

            var res = filteredJobs
                                              .Where(j =>
                                                  // All delays must be completed and unpaid, if there are any delays
                                                  (!j.Delays.Any() || j.Delays.All(d => d.Status == (int)TaskStatusEnum.Completed /*&& d.QAdone == true*/)) &&

                                                  // All purchases must be completed and unpaid, if there are any purchases
                                                  (!j.Purchases.Any() || j.Purchases.All(p => p.Status == (int)TaskStatusEnum.Completed /*&& d.QAdone == true*/))
                                              )
                                              .Select(j => j.Id)
                                              .ToList();

            return res;
        }


    }
}
