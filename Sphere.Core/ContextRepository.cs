using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Sphere.Core
{
    /// <summary>
    /// Repository implementation using a DbContext object to connect to the underlying database.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
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

        /// <summary>
        /// Adds an entity to the memory collection and the underliying database
        /// </summary>
        /// <param name="entity">entity to add</param>
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }
        /// <summary>
        /// Updates an entity from memory collection and the underliying database
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
            context.SaveChanges();
        }        
        /// <summary>
        /// Deletes an entity from memory collection and its underliying database
        /// </summary>
        /// <param name="condition"></param>
        public void Delete(Func<T, bool> condition)
        {
            var entity = Get(condition);
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
        /// <summary>
        /// Gets a single entity based on a boolean condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T Get(Func<T, bool> condition)
        {
            return context.Set<T>().FirstOrDefault(condition);
        }
        /// <summary>
        /// Returns all available objects
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsQueryable();
        }
        /// <summary>
        /// Returns all objects matching the given boolean condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQueryable<T> Find(Func<T, bool> condition)
        {
            var entities = context.Set<T>().Where(condition);
            return entities.AsQueryable();
        }
        /// <summary>
        /// Executes an action query (no return value) against the underlying database.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
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
        /// <summary>
        /// Executes a query against the database
        /// </summary>
        /// <typeparam name="TEntity">the type of the data returned</typeparam>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
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
