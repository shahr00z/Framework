using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.DataLayer
{
    public class Context<T> : IContext<T> where T : class
    {
        private readonly DbContext _dbContext;

        public Context(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool TryGet(Expression<Func<T, bool>> predicate, out T entity)
        {
            entity = List(predicate).SingleOrDefault();
            return entity != null;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return List(predicate).Single();
        }

        public List<T> List(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> result = _dbContext.Set<T>().AsQueryable();
            if (predicate != null)
                result = result.Where(predicate);
            return result.ToList();

        }

        public T Add(T t)
        {
            _dbContext.Entry(t).State = EntityState.Added;
            return t;

        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            List(predicate).ToList().ForEach(p => { _dbContext.Entry(p).State = EntityState.Deleted; });
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public T Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            return t;

        }

        public int Save()
        {
            return _dbContext.SaveChanges();

        }
    }
}