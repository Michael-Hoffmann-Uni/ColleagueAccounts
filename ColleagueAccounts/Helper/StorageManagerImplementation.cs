using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ColleagueAccounts
{
    // An object of this class is responsible for the exchange
    // of data between the app and the environment.
    class StorageManagerImplementation : IStorageManager
    {
        public AccountManager Load(string path, AccountManager accountManager, IValidator validator)
        {
            List<string[]> lineList = File.ReadLines(path).Select(line => line.Split('\t')).ToList();
            // Drops the header/first line from the file
            lineList.RemoveAt(0);
            IAccount account;

            foreach (string[] line in lineList)
            {
                // Makes sure every line consists of 4 parts
                if (line.Length != 4)
                    throw new FormatException();
                string name = line[0];
                string dateString = line[1];
                string amount = line[2];
                string purpose = line[3];
                account = null;

                // Verifies the name.
                if (name.Equals(String.Empty) || name.Length > 20)
                    throw new FormatException("The name of a colleague has to have at least one and a maximum of 20 characters.");

                // Checks if account exists already.
                account = accountManager.AccountList.Find(s => s.Name.Equals(name));
                if (account == null)
                {
                    // Adds the account if it does not exist yet.
                    account = new AccountImplementation(name);
                    accountManager.AddAccount(account);
                }

                // Verifies the date.
                if (!validator.DateCheck(dateString))
                    throw new FormatException("Dates have to be in the format dd.MM.yyyy to be loaded.");
                DateTime date = Convert.ToDateTime(dateString);

                // Verifies the amount.
                if (!validator.MoneyCheck(amount))
                    throw new FormatException("The values of transactions can only have two decimal places.");
                decimal value = Convert.ToDecimal(amount);

                // Verifies the purpose.
                if (purpose.Length > 30)
                    throw new FormatException("A purpose cannot exceed the maximum of 30 characters.");

                // Adds the transaction
                ITransaction transaction = new TransactionImplementation(value, date, purpose);
                account.AddTransaction(transaction);
            }

            return accountManager;
        }

        public void Save(string path, AccountManager accountManager)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Colleague\tDate\tAmount\tPurpose");
            foreach (IAccount account in accountManager.AccountList)
            {
                foreach (ITransaction transaction in account.TransactionList)
                {
                    stringBuilder.AppendLine(account.Name + "\t" + transaction.ToString());
                }
            }
            File.WriteAllText(path, stringBuilder.ToString());
        }
    }
}
