using _2C2P_CodeChallenge.Models;
using _2C2P_CodeChallenge.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2C2P.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactions();
        Task<bool> SaveTransactions(List<TransactionViewModel> transaction);
    }
}
