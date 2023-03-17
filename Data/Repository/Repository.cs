using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ShopBridge.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly Shopbridge_Context dbcontext;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            return dbcontext.Set<T>().AsQueryable();
        }

        public IQueryable<T> Get<T>(params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            var query = AsQueryable<T>();
            return Include(query, navigationProperties);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            var query = AsQueryable<T>();
            query = Include(query, navigationProperties);
            return query.Where(where);
        }

        private static IQueryable<T> Include<T>(IQueryable<T> query, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            if (navigationProperties == null || !navigationProperties.Any())
                return query;

            foreach (var np in navigationProperties)
                query = query.Include(np);

            return query;
        }

        public async Task Delete<T>(T entity) where T : class
        {
            dbcontext.Remove(entity);
            await dbcontext.SaveChangesAsync();
        }

        public async Task Create<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Add(entity);
            await dbcontext.SaveChangesAsync();
        }

        public async Task Update<T>(T entity) where T : class
        {
            dbcontext.Entry(entity).State = EntityState.Modified;
            await dbcontext.SaveChangesAsync();
        }
    }
}
