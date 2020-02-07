using _2C2P_CodeChallenge;
using _2C2P_CodeChallenge.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2C2P.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Transaction> GetTransactions()
        {
            var repo = _unitOfWork.GetRepository<Transaction>();
            return repo.Find(t => t.Id != 0).ToList();
        }

        public bool SaveFile()
        {
            return true;
        }
    }
}
