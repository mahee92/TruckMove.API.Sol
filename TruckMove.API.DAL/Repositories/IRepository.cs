using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity companyModel);
        Task<TEntity> Get(int id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<List<TEntity>> GetAllAsync();



    }
}
