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
        Task<bool> CheckUserEmailExits(string email);

        Task<UserModel> GetUserByEmail(string email);
    }
}
