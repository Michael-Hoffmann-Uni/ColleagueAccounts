using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueAccounts
{
    interface ITransaction
    {
        public decimal Value { get; }
        public DateTime Date { get; }
        public string Purpose { get; }

        // Returns all invormation related to the transaction as a string.
        public string ToString();
    }
}
