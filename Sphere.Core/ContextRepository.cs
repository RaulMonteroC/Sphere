using System;
using System.Data.SqlClient;
using System.Linq;

namespace Sphere.Core
{
    public class ContextRepository<T> : Repository<T> where T : class
    {
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<T, bool> condition)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity Run<TEntity>(string query, params SqlParameter[] sqlParameters)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
