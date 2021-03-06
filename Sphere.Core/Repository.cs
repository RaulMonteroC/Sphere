﻿using System;
using System.Data.SqlClient;
using System.Linq;

namespace Sphere.Core
{
    /// <summary>
    /// Abstraction that defines all avialable operations using a repository.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface Repository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(Func<T, bool> condition);

        T Get(Func<T, bool> condition);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Func<T, bool> condition);

        void Exec(string query, params SqlParameter[] sqlParameters);
        IQueryable<TEntity> Run<TEntity>(string query, params SqlParameter[] sqlParameters);
    }
}
