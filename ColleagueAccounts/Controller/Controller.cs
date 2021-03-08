using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ColleagueAccounts
{
    // An object of this class manages the interaction between the different
    // parts of the application like model, UI and validation.
    internal class Controller
    {
        public Controller()
        {
            this.AccountManager = new AccountManager();
            this.CommandLine = new CommandLine();
            this.Validator = new ValidatorImplementation();
            this.StorageManager = new StorageManagerImplementation();
        }

        internal AccountManager AccountManager { get; set; }
        readonly CommandLine CommandLine;
        readonly IValidator Validator;
        readonly IStorageManager StorageManager;

        public void Launch()
        {
            CommandLine.Start();
            MainMenu();
        }

        private void MainMenu()
        {
            string input = String.Empty;
            while (input != "0")
            {
                input = CommandLine.MainMenu();
                switch (input)
                {
                    case "1":
                        AccountMenu();
                        break;
                    case "2":
                        LoadFile();
                        break;
                    case "3":
                        SaveToFile();
                        break;
                    case "0":
                        CommandLine.Closed();
                        Environment.Exit(0);
                        break;
                    default:
                        CommandLine.Invalid();
                        break;
                }
            }

        }

        private void AccountMenu()
        {
            string input = String.Empty;
            while (input != "0")
            {
                input = CommandLine.AccountMenu(input);
                switch (input)
                {
                    case "1":
                        ShowAllAccounts();
                        break;
                    case "2":
                        ShowTransactions();
                        break;
                    case "3":
                        AddTransaction();
                        break;
                    case "4":
                        AddColleague();
                        break;
                    case "5":
                        RemoveColleague();
                        break;
                    case "0":
                        break;
                    default:
                        CommandLine.Invalid();
                        break;
                }
            }
        }

        private void ShowAllAccounts()
        {
            if (NoAccountsExist())
            {
                CommandLine.NoAccountsExist();
            }
            else
            {
                CommandLine.ShowAll(AccountManager);
            }
        }

        private bool NoAccountsExist()
        {
            if (AccountManager.AccountList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ShowTransactions()
        {
            string name = CommandLine.EnterColleagueName();
            IAccount account = null;
            account = AccountManager.AccountList.Find(s => s.Name.Equals(name));
            // Verifies that the colleague exists.
            if (account == null)
            {
                CommandLine.ColleagueNotExist();
            }
            // Verifies that the colleague has transactions.
            else if (account.TransactionList.Count == 0)
            {
                CommandLine.NoTransactions(account);
            }
            else
            {
                CommandLine.ShowTransactions(account);
            }
        }

        private void AddTransaction()
        {
            string name = CommandLine.EnterColleagueName();
            IAccount account = null;
            account = AccountManager.AccountList.Find(s => s.Name.Equals(name));
            // Verifies that the colleague exists.
            if (account == null)
            {
                CommandLine.ColleagueNotExist();
                return;
            }
            string amount = CommandLine.EnterAmount();
            // Verifies that the entered amount is in the correct format.
            if (!Validator.MoneyCheck(amount))
            {
                CommandLine.InvalidMoneyFormat();
                return;
            }
            decimal value = Convert.ToDecimal(amount);
            string dateString = CommandLine.EnterTransactionDate();
            // Verifies that the entered string resembles a valid date.
            if (!Validator.DateCheck(dateString))
            {
                CommandLine.InvalidDateFormat();
                return;
            }
            DateTime date = Convert.ToDateTime(dateString);
            string purpose = CommandLine.EnterPurpose();
            // Creates a transaction and adds it to the account.
            ITransaction trans = new TransactionImplementation(value, date, purpose);
            account.AddTransaction(trans);
            CommandLine.TransactionSuccess(account.Name, trans.ToString());
        }

        private void AddColleague()
        {
            string name = CommandLine.AddColleague();
            // Verifies that the name is not an empty string.
            if (name.Equals(String.Empty))
            {
                CommandLine.NameIsEmpty();
                return;
            }
            IAccount account = null;
            account = AccountManager.AccountList.Find(s => s.Name.Equals(name));
            // Verifies that the colleague does not exist already.
            if (account != null)
            {
                CommandLine.ColleagueExistsAlready(name);
                return;
            }
            // Creates a new account and adds it to the account-manager.
            account = new AccountImplementation(name);
            AccountManager.AddAccount(account);
            CommandLine.AccountSuccess(name);
        }

        private void RemoveColleague()
        {
            string name = CommandLine.RemoveColleague();
            IAccount account = null;
            account = AccountManager.AccountList.Find(s => s.Name.Equals(name));
            // Verifies that the colleague exists.
            if (account == null)
            {
                CommandLine.ColleagueNotExist();
            }
            else
            {
                AccountManager.RemoveAccount(name);
                CommandLine.AccountRemoveSuccess(name);
            }

        }

        private void SaveToFile()
        {
            // Verifies that there is at least one account to save.
            if (NoAccountsExist())
                CommandLine.NoAccountsExist();
            // Gets the filepath.
            string path = CommandLine.ChooseSaveLocation();
            // Verifies that the directory exists
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                CommandLine.DirectoryNotExist();
                return;
            }
            // Checks if the file already exists.
            if (File.Exists(path))
            {
                string input = CommandLine.FileExistsAlready();
                // Only overwrites file on y or Y input.
                if (!(input.Equals("y") || input.Equals("Y")))
                {
                    CommandLine.FileNotSaved();
                    return;
                }
            }
            // Tries to save the file.
            try
            {
                StorageManager.Save(path, AccountManager);
                CommandLine.FileSaveSuccess();
            }
            catch
            {
                CommandLine.FileSaveFailure();
            }
        }

        private void LoadFile()
        {
            string path = CommandLine.ChooseLoadLocation();
            // Verifies that the file exists.
            if (!File.Exists(path))
            {
                CommandLine.FileNotExist();
                return;
            }
            // Tries to read from file and process data.
            try
            {
                AccountManager = StorageManager.Load(path, AccountManager, Validator);
                CommandLine.FileLoadSuccess();
            }
            catch
            {
                CommandLine.FileLoadFailure();
            }
        }
    }
}
