using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Job> _dbSet;
        private readonly DbSet<JobSequence> _Sequence;
        public JobRepository(DbContextOptions<TrukMoveContext> options)
        {
            
            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<Job>();
            _Sequence = _context.Set<JobSequence>();
        }
        public async Task<int> GetNextJobId()
        {
    
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT NEXT VALUE FOR dbo.JobSeq";
                command.CommandType = CommandType.Text;

                var result = await command.ExecuteScalarAsync();
                return (int)result;
            }


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
                            if (inputNumber == currentValue)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

      
        public async Task<List<JobContact>> GetJobContactsByJobId(int jobId)
        {
            return await _context.Set<JobContact>().Where(x => x.JobId == jobId).ToListAsync();
        }

        public async Task<List<Job>> GetAllJobsByDriverAsync(int driverid, params string[] includeProperties)
        {
            IQueryable<Job> query =  _dbSet;

            //foreach (var include in includeProperties)
            //{
            //    query = query.Include(include);
            //}
            //Add the driver Id
            return await query.Where(e => e.IsActive).ToListAsync();
        }
        public IQueryable<Job> GetAllAsync(int driverId)
        {
            var query = _dbSet.Where(e => e.IsActive && e.Driver == driverId).AsQueryable();

           // string sqlQuery = query.ToQueryString();
            return query;
        }
        public async Task<List<WayPoint>> GetWayPointsByJobId(int jobId)
        {
            return await _context.Set<WayPoint>().Where(x => x.JobId == jobId).ToListAsync();
        }


    }
}
