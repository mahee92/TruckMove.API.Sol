using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.JobRepositories
{
    public class PaymentRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Job> _dbSet;
        private readonly DbSet<JobSequence> _Sequence;
        public PaymentRepository(DbContextOptions<TrukMoveContext> options)
        {

            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<Job>();
            _Sequence = _context.Set<JobSequence>();
        }
        public IQueryable<DriverPurchaseCountDto> GetDriverPurchaseCounts()
        {
            // Step 1: Filter jobs by status 4 or 5
            var filteredJobs = _context.Set<Job>().Where(j => j.Status == 4 || j.Status == 5);

            // Step 2: Join and filter purchases and legs
            var query = from j in filteredJobs
                        join p in _context.Set<Purchase>() on j.Id equals p.JobId into purchaseGroup
                        from pg in purchaseGroup.DefaultIfEmpty()
                        join l in _context.Set<Leg>() on j.Id equals l.JobId into legGroup
                        from lg in legGroup.DefaultIfEmpty()
                        where (pg.IsActive == false || lg.IsActive == false) &&
                              (pg.Driver != null || lg.Driver != null)
                        group new { j.Id, pg.Driver } by new { DriverId = (pg.Driver ?? lg.DriverId), JobId = j.Id } into g
                        select new DriverPurchaseCountDto
                        {
                            DriverId = g.Key.DriverId,
                            JobId = g.Key.JobId,
                            PurchaseCount = g.Count(x => x.Driver != null)
                        };

            // Step 3: Return the filtered and grouped data
            return query.AsQueryable();
        }


        public class DriverPurchaseCountDto
        {
            public int? DriverId { get; set; }
            public int JobId { get; set; }
            public int PurchaseCount { get; set; }
        }
    }
}
