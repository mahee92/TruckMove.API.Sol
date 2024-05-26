using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Job
{
    public class JobRepository : IJobRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<JobModel> _dbSet;
        public JobRepository(DbContextOptions<TrukMoveLocalContext> options)
        {
            _context = new TrukMoveLocalContext(options);
            _dbSet = _context.Set<JobModel>();
        }

        public int GetNextJobId()
        {
            var nextId1 = _context.Database.ExecuteSqlInterpolated($"SELECT NEXT VALUE FOR dbo.JobSeq");
            var nextId = _context.Database.ExecuteSqlRaw("SELECT NEXT VALUE FOR dbo.JobSeq");
            return nextId;
        }
        public bool IsValidSequence(int inputNumber)
        {

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM sys.sequences WHERE name = 'JobSeq'";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    if (result.Read())
                    {
                        // Retrieve the current value and increment by 1 to get the next expected value
                        if (result["current_value"] != DBNull.Value && result["current_value"] is long currentValue)
                        {
                            if(inputNumber==currentValue)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
