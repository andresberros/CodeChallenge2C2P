using _2C2P_CodeChallenge;
using _2C2P_CodeChallenge.Models;
using _2C2P_CodeChallenge.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2P.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            var list = await Task.Run(() => _unitOfWork.GetRepository<Transaction>().Find(t => t.TransactionId != null).ToList());
            return list;
        }

        public async Task<bool> SaveTransaction(TransactionViewModel transaction)
        {
            if (transaction != null && !string.IsNullOrWhiteSpace(transaction.TransactionId))
            {
                if (!_unitOfWork.GetRepository<Transaction>().Exist(t => t.TransactionId == transaction.TransactionId))
                {
                    var newTransaction = new Transaction()
                    {
                        TransactionId = transaction.TransactionId,
                        Status = transaction.Status.ToString(),
                        Amount = transaction.Amount,
                        CurrencyCode = transaction.CurrencyCode,
                        TransactionDate = transaction.TransactionDate
                    };

                    _unitOfWork.GetRepository<Transaction>().Add(newTransaction);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
            }

            return false;
        }
    }
}
