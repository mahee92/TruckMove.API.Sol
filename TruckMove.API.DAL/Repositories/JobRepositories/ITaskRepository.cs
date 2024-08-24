using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public interface ITaskRepository
    {
        Task<List<Accommodation>> GetAccommodationTasksByUserId(int userId);
        Task<List<PermitsAndPlate>> GetPermitsAndPlateTasksByUserId(int userId);
        Task<List<PublicTransport>> GetPublicTransportTasksByUserId(int userId);
        Task<List<Purchase>> GetPurchaseTasksByUserId(int userId);
        Task<List<Job>> GetJobsByUserId(int userId);
        Task<List<Job>> GetDrivingTasksByUserId(int userId);

        Task<Dictionary<DateTime, int>> GetUpcommingJobsByPickupDate();
    }
}
