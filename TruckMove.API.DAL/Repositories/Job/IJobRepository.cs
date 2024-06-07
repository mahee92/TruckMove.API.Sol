using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Job
{
    public interface IJobRepository
    {
        Task<TruckMove.API.DAL.Models.Job> GetJobById(int id);
        Task<int> GetNextJobId();
        bool IsValidSequence(int inputNumber);

    }
}
