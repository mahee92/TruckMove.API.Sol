using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Job
{
    public class JobRepository : IJobRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<TruckMove.API.DAL.Models.Job> _dbSet;
        private readonly DbSet<TruckMove.API.DAL.Models.JobSequence> _Sequence;
        public JobRepository(DbContextOptions<TrukMoveContext> options)
        {
            
            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<TruckMove.API.DAL.Models.Job>();
            _Sequence = _context.Set<TruckMove.API.DAL.Models.JobSequence>();
        }

        public async Task<TruckMove.API.DAL.Models.Job> GetJobById(int Id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == Id && x.IsActive == true);
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
    }
}
