using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueAccounts
{
    // An object of this class represents the exchange of money between
    // the user and a colleague.
    class TransactionImplementation : ITransaction
    {
        public TransactionImplementation(decimal value, DateTime date, string purpose)
        {
            this.Value = value;
            this.Date = date;
            if (purpose == null)
            {
                this.Purpose = String.Empty;
            }
            else
            {
                this.Purpose = purpose;
            }
        }

        public decimal Value { get; }
        public DateTime Date { get; }
        public string Purpose { get; }

        public override string ToString()
        {
            return Date.ToString("dd/MM/yyyy") + "\t" + Value.ToString() + "\t" + Purpose;
        }

        public override bool Equals(object obj)
        {
            return obj is TransactionImplementation transaction &&
                   Value == transaction.Value &&
                   Date == transaction.Date &&
                   Purpose.Equals(transaction.Purpose);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Date, Purpose);
        }
    }
}
