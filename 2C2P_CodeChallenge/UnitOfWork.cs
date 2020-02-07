using _2C2P_CodeChallenge.Models;
using _2C2P_CodeChallenge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2P_CodeChallenge
{
    public class UnitOfWork : IUnitOfWork
    {
        private Entities _context = new Entities();
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork()
        {
            _repositories = new Dictionary<Type, object>();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IRepository<TSet> GetRepository<TSet>() where TSet : class
        {
            if (_repositories.Keys.Contains(typeof(TSet)))
                return _repositories[typeof(TSet)] as IRepository<TSet>;

            var repository = new Repository<TSet>(_context);
            _repositories.Add(typeof(TSet), repository);
            return repository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}