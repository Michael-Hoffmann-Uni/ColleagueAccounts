using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ColleagueAccounts
{
    // An object of this class stores colleagues.
    // It has methods to manage (add/remove/print) its colleagues.
    class AccountManager : Collection<IAccount>
    {
        public AccountManager()
        {
            this.AccountList = new List<IAccount>();
        }

        public List<IAccount> AccountList { get; private set; }

        public void AddAccount(IAccount account)
        {
            AccountList.Add(account);
        }

        public void RemoveAccount(string name)
        {
            if (AccountList.FindAll(s => s.Name.Equals(name)).Count == 0)
                throw new ArgumentException("No account with that name exists.");
            AccountList.RemoveAll(s => s.Name.Equals(name));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IAccount account in AccountList)
            {
                stringBuilder.AppendLine(account.ToStringHeader());
            }
            return stringBuilder.ToString();
        }
    }
}
