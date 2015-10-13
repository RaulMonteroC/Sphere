﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sphere.Core
{
    public class FakeRepository<T> : Repository<T> where T : class
    {
        private ICollection<T> storage;

        public FakeRepository()
        {
            storage = new List<T>();
        }

        public void Add(T entity)
        {
            storage.Add(entity);
        }

        public void Update(T entity)
        {
            storage.Remove(entity);
            storage.Add(entity);
        }

        public void Delete(Func<T, bool> condition)
        {
            var entity = storage.FirstOrDefault(condition);
            storage.Remove(entity);
        }

        public T Get(Func<T, bool> condition)
        {
            return storage.FirstOrDefault(condition);
        }

        public IQueryable<T> GetAll()
        {
            return storage.AsQueryable();
        }

        public IQueryable<T> Find(Func<T, bool> condition)
        {
            return storage.Where(condition).AsQueryable();
        }

        public T FindSingle(Func<T, bool> condition)
        {
            return storage.FirstOrDefault(condition);
        }

        public void Exec(string query, params System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            
        }

        public IQueryable<TEntity> Run<TEntity>(string query, params System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            return new List<TEntity>().AsQueryable();
        }
    }
}
