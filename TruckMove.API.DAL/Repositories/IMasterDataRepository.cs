using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TaskStatus = TruckMove.API.DAL.Models.TaskStatus;

namespace TruckMove.API.DAL.Repositories
{
    public interface IMasterDataRepository
    {
        Task<List<Role>> GetAllRoles();
        Task<List<User>> GetUsersByRolesAsync(List<int> roleIds);

        Task<List<HookupType>> GetAllRolesHookupTypes();

        Task<List<JobStatus>> GetAllJobStatus();
        Task<JobStatus> GetJobStatus(int id);
        Task<List<PublicTransportType>> GetPublicTransportTypes();

        Task<List<TaskStatus>> GetAllTasStatuses();

    }
}
