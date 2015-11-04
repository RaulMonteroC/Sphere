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

        public ContextRepository()
        {
            this.context = SphereConfig.GlobalContext;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(Func<T, bool> condition)
        {
            var entity = Get(condition);
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
            context.SaveChanges();
        }        

        public T Get(Func<T, bool> condition)
        {
            return context.Set<T>().FirstOrDefault(condition);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsQueryable();
        }

        public IQueryable<T> Find(Func<T, bool> condition)
        {
            var entities = context.Set<T>().Where(condition);
            return entities.AsQueryable();
        }

        public void Exec(string query, params SqlParameter[] sqlParameters)
        {
            if (sqlParameters != null)
            {
                context.Database.ExecuteSqlCommand(query,sqlParameters);
            }
            else
            {
                context.Database.ExecuteSqlCommand(query);
            }
        }

        public IQueryable<TEntity> Run<TEntity>(string query, params SqlParameter[] sqlParameters)
        {
            if(sqlParameters != null)
            {
                return context.Database.SqlQuery<TEntity>(query,sqlParameters).AsQueryable();
            }
            else
            {
                return context.Database.SqlQuery<TEntity>(query).AsQueryable();
            }
            
        }
    }
}
