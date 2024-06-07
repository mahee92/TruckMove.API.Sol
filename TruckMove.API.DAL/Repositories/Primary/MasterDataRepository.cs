using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Role> _RoledbSet;
        public MasterDataRepository(DbContextOptions<TrukMoveContext> options)
        {
            _context = new TrukMoveContext(options);
            _RoledbSet = _context.Set<Role>();

        }
        // create method to get all roles
        public async Task<List<Role>> GetAllRoles()
        {
            return await _RoledbSet.ToListAsync();
        }

      

    }
}
