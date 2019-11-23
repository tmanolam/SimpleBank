using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.Models
{
    public enum CreditType
    {
        Deposit,
        TransferIn,
        Interest
    }

    public enum DebitType
    {
        Withdraw,
        TransferOut,
        Fee
    }

    public class Transaction
    {
        public int TransactionID { get; set; }
        public string BankAccountID { get; set; }
        public DateTime TransactionDate { get; set; }
        public CreditType? CreditType { get; set; }
        public DebitType? DebitType { get; set; }
        public double Amount { get; set; }
        public double PreviousBalance { get; set; }
        public double NewBalance { get; set; }
        public BankAccount Account { get; set; }
    }
}
