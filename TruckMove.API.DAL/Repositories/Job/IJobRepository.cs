using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.DAL.Repositories.Job
{
    public interface IJobRepository
    {
        Task<int> GetNextJobId();
        bool IsValidSequence(int inputNumber);

    }
}
