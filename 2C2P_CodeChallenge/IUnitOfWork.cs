using _2C2P_CodeChallenge.Repository;
using System.Threading.Tasks;

namespace _2C2P_CodeChallenge
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IRepository<TSet> GetRepository<TSet>() where TSet : class;
    }
}