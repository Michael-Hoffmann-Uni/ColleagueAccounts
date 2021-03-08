using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ColleagueAccounts
{
    // An object of this class represents a colleague and
    // stores all transactions between this colleague and the user.
    // It has methods to manage (add/remove/print) its transactions.
    class AccountImplementation : IAccount
    {
        public AccountImplementation(string name)
        {
            if (name == null)
                throw new ArgumentNullException("The name of an account must not be null.");
            if (name.Equals(String.Empty))
                throw new ArgumentException("The name of an account must not be empty.");
            this.Name = name;
            this.Balance = 0;
            this.TransactionList = new List<ITransaction>();
        }

        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public List<ITransaction> TransactionList { get; private set; }

        public void AddTransaction(ITransaction transaction)
        {
            TransactionList.Add(transaction);
            Balance += transaction.Value;
        }

        private void SortTransactions()
        {
            this.TransactionList.OrderBy(Transaction => Transaction.Date);
            this.TransactionList.Reverse();
        }

        // Returns the account-name and balance as a string.
        public string ToStringHeader()
        {
            return "Colleague: " + Name + "\t\t\tBalance: " + Balance + " \u20AC";
        }

        // Returns all transactions related to the account as a string.
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ITransaction transaction in TransactionList)
            {
                stringBuilder.AppendLine(Name + "\t" + transaction.ToString());
            }
            this.SortTransactions();
            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is AccountImplementation account &&
                   Name.Equals(account.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
