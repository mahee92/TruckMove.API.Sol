using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<UserModel> _dbSet;
        public UserRepository(DbContextOptions<TrukMoveLocalContext> options)
        {
            _context = new TrukMoveLocalContext(options);
            _dbSet = _context.Set<UserModel>();
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
