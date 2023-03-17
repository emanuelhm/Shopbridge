using ShopBridge.Domain.Models;
using System.Linq.Expressions;

namespace ShopBridge.Data.Repository
{
    public interface IRepository
    {
        IQueryable<T> AsQueryable<T>() where T : class;
        IQueryable<T> Get<T>(params Expression<Func<T, object>>[] navigationProperties) where T : class;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Create<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
    }
}
