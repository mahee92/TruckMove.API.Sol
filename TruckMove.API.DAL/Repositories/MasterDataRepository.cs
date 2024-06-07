//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TruckMove.API.DAL.Models;

//namespace TruckMove.API.DAL.Repositories
//{
//    public class MasterDataRepository : IMasterDataRepository
//    {
//        private readonly DbContext _context;
//        private readonly DbSet<RoleModel> _roleModeldbSet;
//        private readonly DbSet<UserModel> _userModeldbSet;
//        public MasterDataRepository(DbContextOptions<TrukMoveContext> options)
//        {
//            _context = new TrukMoveContext(options);
//            _roleModeldbSet = _context.Set<RoleModel>();
//            _userModeldbSet = _context.Set<UserModel>();

//        }
//        // create method to get all roles
//        public async Task<List<RoleModel>> GetAllRoles()
//        {
//            return await _roleModeldbSet.ToListAsync();
//        }
//        public async Task<List<UserModel>> GetUsersByRoleAsync(int roleId)
//        {
//            return await _userModeldbSet
//                .Include(u => u.UserRoles)
//                .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId && ur.IsActive) && u.IsActive)
//                .ToListAsync();
//        }


//    }
//}
