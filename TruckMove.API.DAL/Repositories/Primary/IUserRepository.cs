using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public interface IUserRepository
    {
        Task AddRolesAsync(int id, List<int> roles);
        Task<bool> CheckUserEmailExits(string email);
        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByEmailWithRoles(string email);

        Task<List<Role>> GetRolesByUserId(int id);
        
    }
}
