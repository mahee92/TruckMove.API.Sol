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
                                                                  x.Job.Status < (int)JobStatusEnum.QADone &&
                                                                  x.OrganizeNow == false &&
                                                                  x.Assignee == userId &&
                                                                  x.Status != (int)TaskStatusEnum.Completed &&
                                                                  x.IsActive == true)
                                                            .Select(x => new Accommodation
                                                            {   JobId=x.JobId,
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
                                                                   x.Job.Status < (int)JobStatusEnum.QADone &&
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
                                                                   x.Job.Status < (int)JobStatusEnum.QADone &&
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
                                                                   x.Job.Status < (int)JobStatusEnum.QADone &&
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
        public async Task<List<Job>> GetJobTasksByUserId(int userId)
        {
            return await _context.Set<Job>()
                                 .Where(x => x.IsActive == true &&
                                             x.Controller == userId &&
                                             x.Status != (int)JobStatusEnum.Completed)
                                 .Select(x => new Job
                                 {
                                     Id = x.Id,
                                     PickupLocation = x.PickupLocation,
                                     DropOfLocation = x.DropOfLocation,
                                     VehicleNavigation = x.VehicleNavigation,
                                     StatusNavigation = x.StatusNavigation,
                                     DriverNavigation = x.DriverNavigation,
                                     QALegCount = x.Legs.Count(y => y.Status == (int)TaskStatusEnum.Completed),
                                 })
                                 .ToListAsync();
        }
    }
}
