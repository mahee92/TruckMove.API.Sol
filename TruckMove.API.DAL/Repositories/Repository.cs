using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IActiveEntity
    { 

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContextOptions<TrukMoveContext> options)
        {
            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<TEntity>();
        }

        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            await updateJob(entity);



            return entity;

        }

        public async Task updateJob(TEntity entity)
        {
            try
            {
                if (entity is IJobUpdatable jobUpdatable && jobUpdatable.ShouldUpdateJob)
                {
                    // Retrieve the job entity using the JobId from the updatable entity
                    var job = await _context.Set<Job>().FindAsync(jobUpdatable.JobId);
                    if (job != null)
                    {
                        if (entity is AuditableEntity auditableEntity)
                        {

                            job.UpdatedById = auditableEntity.UpdatedById; // Set this to the actual user
                            job.LastModifiedDate = DateTime.UtcNow;
                        }


                        // Call any additional update logic if needed
                        //jobUpdatable.UpdateJob(job);

                        // Save changes to the Job table
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                // do nothing 
            }


        }


        public async Task<TEntity> GetAsync(int id, bool checkActive = true)
        {
          if(checkActive)
            {
                return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
            }
            else
            {
                return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            }
          
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await updateJob(entity);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
             await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }

        }
        //public async Task DeleteAsync(int id)
        //{
        //    // Load the entity with its related entities
        //    var entity = await _dbSet
        //        .Include(e => e.RelatedEntities) // Include the related entities
        //        .FirstOrDefaultAsync(e => e.Id == id);

        //    if (entity != null)
        //    {
        //        // Delete the related entities first
        //        if (entity.RelatedEntities != null)
        //        {
        //            _context.RelatedEntities.RemoveRange(entity.RelatedEntities);
        //        }

        //        // Delete the main entity
        //        _dbSet.Remove(entity);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.Where(e => e.IsActive).OrderByDescending(x=>x.CreatedDate).ToListAsync();
        }
       
        //public async Task<TEntity> GetWithIncludesAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        //}

        //public async Task<List<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.Where(e => e.IsActive).ToListAsync();
        //}
        //public async Task<List<TEntity>> GetAllWithNestedIncludesAsync(params string[] includeProperties)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    return await query.Where(e => e.IsActive).ToListAsync();
        //}
        public async Task<TEntity> GetWithNestedIncludesAsync(int id, params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }
        public async Task DeleteByIdsAsync(IEnumerable<int> ids)
        {
            var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.ToList();
        }

        public async Task<string> GetPropertyAsync(int id, string propertyName)
        {

            // Build a query to select only the specific property
            var query = _dbSet
                .Where(e => EF.Property<int>(e, "Id") == id && EF.Property<bool>(e, "IsActive"))
                .Select(e => EF.Property<object>(e, propertyName))
                .AsQueryable();

            // Execute the query and get the property value
            var value = await query.FirstOrDefaultAsync();
            if(value == null)
            {
                return "-1";
            }

            return value?.ToString();
        }
    }
}
