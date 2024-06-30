using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Role> _roleModeldbSet;
        private readonly DbSet<User> _userModeldbSet;
        private readonly DbSet<HookupType> _hookuptype;
        public MasterDataRepository(DbContextOptions<TrukMoveContext> options)
        {
            _context = new TrukMoveContext(options);
            _roleModeldbSet = _context.Set<Role>();
            _userModeldbSet = _context.Set<User>();
            _hookuptype = _context.Set<HookupType>();

        }
        // create method to get all roles
        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleModeldbSet.ToListAsync();
        }
        public async Task<List<User>> GetUsersByRoleAsync(int roleId)
        {
            return await _userModeldbSet
                .Include(u => u.UserRoleUsers)
                .Where(u => u.UserRoleUsers.Any(ur => ur.RoleId == roleId && ur.IsActive) && u.IsActive)
                .ToListAsync();
        }
        public async Task<List<HookupType>> GetAllRolesHookupTypes()
        {
            return await _hookuptype.ToListAsync();
        }

    }
}
