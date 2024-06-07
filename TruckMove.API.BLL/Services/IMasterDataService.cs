using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Services
{
    public interface IMasterDataService
    {
        Task<Response<RoleDto>> GetRolesAsync();
        Task<Response<UserOutputDto>> GetUsersByRoleAsync(RoleEnum role);
    }
}
