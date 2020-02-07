using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2C2P_CodeChallenge.ViewModels
{
    public class TransactionViewModel
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }
    }
}