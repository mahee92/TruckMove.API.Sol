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
            return entity;

        }

       
        public async Task<TEntity> GetAsync(int id)
        {
          
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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



    }
}
