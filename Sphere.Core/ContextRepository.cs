using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Sphere.Core
{
    public class ContextRepository<T> : Repository<T> where T : class
    {
        private DbContext context;

        public ContextRepository(DbContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(Func<T, bool> condition)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Exec(string query, params SqlParameter[] sqlParameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(Func<T, bool> condition)
        {
            throw new NotImplementedException();
        }

        public T Get(Func<T, bool> condition)
        {
            return context.Set<T>().FirstOrDefault(condition);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsQueryable();
        }

        public TEntity Run<TEntity>(string query, params SqlParameter[] sqlParameters)
        {
            throw new NotImplementedException();
        }       
    }
}
