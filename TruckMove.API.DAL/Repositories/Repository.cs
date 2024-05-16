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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContextOptions<TrukMoveLocalContext> options)
        {
            _context = new TrukMoveLocalContext(options);
            _dbSet = _context.Set<TEntity>();
        }

        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

       
        public async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

   
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


    }
}
