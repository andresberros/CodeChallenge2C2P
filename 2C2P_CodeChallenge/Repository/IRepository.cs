using System;
using System.Linq;
using System.Linq.Expressions;

namespace _2C2P_CodeChallenge.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        T FindSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties);
        int Count(Expression<Func<T, bool>> predicate = null);
        bool Exist(Expression<Func<T, bool>> predicate = null);
        void Detach(T entity);
    }
}