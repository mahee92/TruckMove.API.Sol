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
        IQueryable<Job> GetAllAsync();
        Task<List<WayPoint>> GetWayPointsByJobId(int jobId);

        Task DeleteWaypointsByIdsAsync(IEnumerable<int> ids);
        Task<List<WayPoint>> AddWaypointsRangeAsync(List<WayPoint> entities);

        Task Acknowledge(int legId,int JobId);

       Task<int> GetNextLegNumber(int jobId);

       Task<bool> CheckAnyOngoingLegs(int jobId);

        Task<bool> CheckDriverHasOngoingLegs(int jobId, int driverId);
        Task<List<Leg>> GetLegsByJobId(int jobId);

        Task DeleteCheckListIagesByCheckListId(int checkListId);
        Task AddCheckListImages(List<CheckListImage> checkListimagses);
    }
}
