using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;

namespace TruckMove.API.BLL.Services
{
    public interface IMasterDataService
    {
        Task<Response<RoleDto>> GetRolesAsync();
    }
}
