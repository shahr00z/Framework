using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.DataLayer
{
    public interface IContext<T>
    {
        bool TryGet(Expression<Func<T, bool>> predicate, out T entity);
        T Get(Expression<Func<T, bool>> predicate);
        List<T> List(Expression<Func<T, bool>> predicate = null);
        T Add(T t);
        void Delete(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        T Update(T t);
        int Save();
    }
}