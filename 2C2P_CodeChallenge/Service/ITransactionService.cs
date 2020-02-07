using _2C2P_CodeChallenge.Models;
using System.Collections.Generic;

namespace _2C2P.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetTransactions();
        bool SaveFile();
    }
}
