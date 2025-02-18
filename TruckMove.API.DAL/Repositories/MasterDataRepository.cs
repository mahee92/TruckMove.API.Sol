using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TruckMove.API.DAL.Models;
using TaskStatus = TruckMove.API.DAL.Models.TaskStatus;

namespace TruckMove.API.DAL.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Role> _roleModeldbSet;
        private readonly DbSet<User> _userModeldbSet;
        private readonly DbSet<HookupType> _hookuptype;
        private readonly DbSet<JobStatus> _jobStatus;
        private readonly DbSet<PublicTransportType> _publicTransport;
        private readonly DbSet<TaskStatus> _publicTaskStatus;
        public MasterDataRepository(DbContextOptions<TrukMoveContext> options)
        {
            _context = new TrukMoveContext(options);
            _roleModeldbSet = _context.Set<Role>();
            _userModeldbSet = _context.Set<User>();
            _hookuptype = _context.Set<HookupType>();
            _jobStatus = _context.Set<JobStatus>();
            _publicTransport = _context.Set<PublicTransportType>();
            _publicTaskStatus = _context.Set<TaskStatus>();

        }
        // create method to get all roles
        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleModeldbSet.ToListAsync();
        }

        public async Task<List<User>> GetUsersByRolesAsync(List<int> roleIds)
        {
            return await _userModeldbSet
                .Include(u => u.UserRoleUsers)
                .Where(u => u.UserRoleUsers.Any(ur => roleIds.Contains(ur.RoleId) && ur.IsActive) && u.IsActive)
                .ToListAsync();
        }
        public async Task<List<HookupType>> GetAllRolesHookupTypes()
        {
            return await _hookuptype.ToListAsync();
        }
        public async Task<List<JobStatus>> GetAllJobStatus()
        {
            return await _jobStatus.ToListAsync();
        }
        public async Task<JobStatus> GetJobStatus(int id)
        {
            return await _jobStatus.Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<List<PublicTransportType>> GetPublicTransportTypes()
        {
            return await _publicTransport.ToListAsync();
        }
        public async Task<List<TaskStatus>> GetAllTasStatuses()
        {
            return await _publicTaskStatus.ToListAsync();
        }

        public async Task<List<Rates>> GetAllRates()
        {
            return await _context.Set<Rates>().ToListAsync();
        }
        public async Task<List<PaymentStatus>> GetAllPaymentStatus()
        {
            return await _context.Set<PaymentStatus>().ToListAsync();
        }
    }
}
