using Microsoft.EntityFrameworkCore;
using TruckMove.API.DAL.Models;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public class TaskRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Job> _dbSet;
        private readonly DbSet<JobSequence> _Sequence;
        public JobRepository(DbContextOptions<TrukMoveContext> options)
        {

            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<Job>();
            _Sequence = _context.Set<JobSequence>();
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
    }
}
