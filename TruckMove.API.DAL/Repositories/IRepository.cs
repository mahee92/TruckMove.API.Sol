//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using TruckMove.API.DAL.Models;

//namespace TruckMove.API.DAL.Repositories
//{
//    public interface IRepository<TEntity> where TEntity : class
//    {
//        Task<TEntity> AddAsync(TEntity model);
//        Task<TEntity> GetAsync(int id);
//        Task UpdateAsync(TEntity entity);
//        Task DeleteAsync(TEntity entity);
//        Task<List<TEntity>> GetAllAsync();
//        Task<TEntity> GetWithIncludesAsync(int id, params Expression<Func<TEntity, object>>[] includes);

//        Task<List<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includes);
        
//    }
//}
