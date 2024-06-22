using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public interface IJobRepository
    {
      
        Task<int> GetNextJobId();
        bool IsValidSequence(int inputNumber);

        Task<List<JobContact>> GetJobContactsByJobId(int jobId);
        Task<List<Job>> GetAllJobsByDriverAsync(int driverid, params string[] includeProperties);

        IQueryable<Job> GetAllAsync(int driverId);
        Task<List<WayPoint>> GetWayPointsByJobId(int jobId);
    }
}
