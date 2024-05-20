using Microsoft.EntityFrameworkCore;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<UserModel> _dbSet;
        private readonly DbSet<UserRoleModel> _roleModeldbSet;
        public UserRepository(DbContextOptions<TrukMoveLocalContext> options)
        {
            _context = new TrukMoveLocalContext(options);
            _dbSet = _context.Set<UserModel>();
            _roleModeldbSet = _context.Set<UserRoleModel>();
        }

        public async Task AddRolesAsync(int id, List<int> roles)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();
            var userRoles = await _roleModeldbSet.Where(x => x.UserId == id).ToListAsync();
            foreach (var role in userRoles)
            {
                role.IsActive = false;
            }

            await _context.SaveChangesAsync();


            foreach (var roleId in roles)
            {
                var role = userRoles.FirstOrDefault(x => x.RoleId == roleId);
                if (role != null)
                {
                    role.IsActive = true;
                }
                else
                {
                    var userRole = new UserRoleModel
                    {
                        UserId = id,
                        RoleId = roleId,
                        IsActive = true
                    };
                    await _roleModeldbSet.AddAsync(userRole);
                }

            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

        }


        public async Task<bool> CheckUserEmailExits(string email)
        {
            return await _dbSet.AnyAsync(user => user.Email == email && user.IsActive);
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(user => user.Email == email && user.IsActive);
        }
    }
}
