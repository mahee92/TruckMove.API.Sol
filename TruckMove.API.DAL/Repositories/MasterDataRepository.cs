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
        private readonly DbSet<RoleModel> _roleModeldbSet;
        public MasterDataRepository(DbContextOptions<TrukMoveLocalContext> options)
        {
            _context = new TrukMoveLocalContext(options);
            _roleModeldbSet = _context.Set<RoleModel>();

        }
        // create method to get all roles
        public async Task<List<RoleModel>> GetAllRoles()
        {
            return await _roleModeldbSet.ToListAsync();
        }



    }
}
