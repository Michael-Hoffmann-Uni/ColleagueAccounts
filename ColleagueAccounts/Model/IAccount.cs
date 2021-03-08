using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueAccounts
{
    interface IAccount
    {
        public string Name { get; }
        public decimal Balance { get; }
        public List<ITransaction> TransactionList { get; }

        public void AddTransaction(ITransaction trans);

        // ToStringHeader() serves the purpose of printing general account information.
        public string ToStringHeader();

        // ToString() serves the purpose of printing detailed information related to the account.
        public string ToString();
    }
}
