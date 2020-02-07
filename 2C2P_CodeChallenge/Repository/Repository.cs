using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace _2C2P_CodeChallenge.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; private set; }
        private readonly IDbSet<T> _dbset;

        public Repository(DbContext context)
        {
            Context = context;
            _dbset = Context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dbset.Attach(entity);
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public T FindSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ?
                   set.FirstOrDefault() :
                   set.FirstOrDefault(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ? set : set.Where(predicate);
        }

        public IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties != null)
                foreach (var include in includeProperties)
                    _dbset.Include(include);

            return _dbset;
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate == null) ?
                   _dbset.Count() :
                   _dbset.Count(predicate);
        }

        public bool Exist(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate == null) ? _dbset.Any() : _dbset.Any(predicate);
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void Detach(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }
    }
}