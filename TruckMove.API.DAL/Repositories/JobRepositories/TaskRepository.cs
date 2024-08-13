using Microsoft.EntityFrameworkCore;
using TruckMove.API.DAL.Models;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContext _context;
        public TaskRepository(DbContextOptions<TrukMoveContext> options)
        {

            _context = new TrukMoveContext(options);
          
        }

        public async Task<List<Accommodation>> GetAccommodationTasksByUserId(int userId)
        {
            return await _context.Set<Accommodation>().Where(x => x.OrganizeNow == false && x.Assignee == userId && x.Status != (int)TaskStatusEnum.Completed && x.IsActive==true).ToListAsync();
        }
        public async Task<List<PermitsAndPlate>> GetPermitsAndPlateTasksByUserId(int userId)
        {
            return await _context.Set<PermitsAndPlate>().Where(x => x.OrganizeNow == false && x.Assignee == userId && x.Status != (int)TaskStatusEnum.Completed && x.IsActive == true).ToListAsync();
        }
        public async Task<List<PublicTransport>> GetPublicTransportTasksByUserId(int userId)
        {
            return await _context.Set<PublicTransport>().Where(x => x.OrganizeNow == false && x.Assignee == userId && x.Status != (int)TaskStatusEnum.Completed && x.IsActive == true).ToListAsync();
        }
        public async Task<List<Purchase>> GetPurchaseTasksByUserId(int userId)
        {
            return await _context.Set<Purchase>().Where(x => x.OrganizeNow == false && x.Assignee == userId && x.Status != (int)TaskStatusEnum.Completed && x.IsActive == true).ToListAsync();
        }
        public async Task<List<Job>> GetJobTasksByUserId(int userId)
        {
            return await _context.Set<Job>().Where(x =>  x.IsActive == true
                                                         && x.Controller== userId).ToListAsync();

            //x.Status == (int)JobStatusEnum.ArrivalChecked ||
            //x.Status == (int)JobStatusEnum.QADone || 
            //x.Status == (int)JobStatusEnum.PaymentDone)
            /*&&*/
        }
    }
}
