using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.PrimaryRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly DbSet<UserRole> _RoledbSet;
        public UserRepository(DbContextOptions<TrukMoveContext> options)
        {
            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<User>();
            _RoledbSet = _context.Set<UserRole>();
        }

        public async Task AddRolesAsync(int id, List<int> roles)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();
            var userRoles = await _RoledbSet.Where(x => x.UserId == id).ToListAsync();
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
                    var userRole = new UserRole
                    {
                        UserId = id,
                        RoleId = roleId,
                        IsActive = true
                    };
                    await _RoledbSet.AddAsync(userRole);
                }

            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

        }


        public async Task<bool> CheckUserEmailExits(string email)
        {
            return await _dbSet.AnyAsync(user => user.Email == email && user.IsActive);
        }


        public async Task<List<Role>> GetRolesByUserId(int id)
        {
            var userRoles = await _RoledbSet
                .Where(ur => ur.UserId == id && ur.IsActive)
                .Include(ur => ur.Role)
                .ToListAsync();

            var roles = userRoles.Select(ur => ur.Role).ToList();
            return roles;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(user => user.Email == email && user.IsActive);
        }
        public async Task<User> GetUserByEmailWithRoles(string email)
        {
            
            return await _dbSet
              .Include(u => u.UserRoleUsers)
              .ThenInclude(ur => ur.Role)
              .FirstOrDefaultAsync(user => user.Email == email && user.IsActive);

        }
    }
}
