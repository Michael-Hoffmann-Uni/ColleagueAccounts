using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueAccounts
{
    interface IStorageManager
    {
        // Loads data and adds loaded data to the account-manager.
        public AccountManager Load(string path, AccountManager accountManager, IValidator validator);

        // Saves data to a provided path.
        public void Save(string path, AccountManager accountManager);
    }
}
