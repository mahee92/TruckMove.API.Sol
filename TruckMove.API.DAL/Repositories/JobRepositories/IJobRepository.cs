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
        

    }
}
