using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.DAL.Models;
using static TruckMove.API.DAL.MasterData.MasterData;
using TaskStatus = TruckMove.API.DAL.Models.TaskStatus;

namespace TruckMove.API.BLL.Services
{
    public interface IMasterDataService
    {
        Task<Response<RoleDto>> GetRolesAsync();
        Task<Response<UserOutputDto>> GetUsersByRoleAsync(List<RoleEnum> roles);
     

        Task<Response<HookupType>> GetAllHookupTypes();
        Task<Response<PublicTransportType>> GetAllPublicTransportTypes();

        Task<Response<TaskStatus>> GetAllTaskStatus();
    }
}
