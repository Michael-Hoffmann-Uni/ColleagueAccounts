using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ColleagueAccounts
{
    // An object of this class prints text for the user and
    // accepts keyboard input.
    class CommandLine
    {
        public void Start()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.WriteLine("Welcome to Colleague Accounts\n");
        }

        public string MainMenu()
        {
            System.Console.WriteLine("\nPlease choose an option by entering its number:");
            System.Console.WriteLine("1 Manage colleagues");
            System.Console.WriteLine("2 Import data");
            System.Console.WriteLine("3 Export data");
            System.Console.WriteLine("0 Exit\n");
            return System.Console.ReadLine();
        }

        public void Invalid()
        {
            System.Console.WriteLine("Invalid input.");
        }

        public void Closed()
        {
            System.Console.WriteLine("Program has been closed.");
        }

        public string AccountMenu(string input)
        {
            System.Console.WriteLine("\nPlease choose an option by entering its number:");
            System.Console.WriteLine("1 Show all colleagues with balances");
            System.Console.WriteLine("2 Show transactions for a colleague");
            System.Console.WriteLine("3 Add a transaction");
            System.Console.WriteLine("4 Add a colleague");
            System.Console.WriteLine("5 Remove a colleague");
            System.Console.WriteLine("0 Back\n");
            return System.Console.ReadLine();
        }

        public void NoAccountsExist()
        {
            System.Console.WriteLine("There are currently no colleagues in the system.");
            Pause();
        }

        public void ShowAll(AccountManager accountManager)
        {
            System.Console.WriteLine("\n" + accountManager.ToString());
            Pause();
        }

        public string EnterColleagueName()
        {
            System.Console.Write("Please enter the colleagues name:\n");
            return System.Console.ReadLine();
        }

        public void ColleagueNotExist()
        {
            System.Console.WriteLine("\nA colleague with that name does not exist.");
            Pause();
        }

        public void NoTransactions(IAccount account)
        {
            System.Console.WriteLine("\n" + account.ToStringHeader());
            System.Console.WriteLine("This colleague has no transactions.");
            Pause();
        }

        public void ShowTransactions(IAccount account)
        {
            System.Console.WriteLine("\n" + account.ToStringHeader());
            System.Console.WriteLine(account.ToString());
            Pause();
        }

        public string EnterAmount()
        {
            System.Console.Write("\nPlease enter the amount of money in Euro, with a comma as the decimal separator:\n");
            System.Console.Write("(A positive value means you received money back)\n");
            return System.Console.ReadLine();
        }

        public void InvalidMoneyFormat()
        {
            System.Console.WriteLine("The amount has an invalid format.");
            Pause();
        }

        public string EnterTransactionDate()
        {
            System.Console.Write("Please enter the date of transaction:\n");
            System.Console.Write("(Format: dd.MM.yyyy)\n");
            return System.Console.ReadLine();
        }

        public void InvalidDateFormat()
        {
            System.Console.WriteLine("The date has an invalid format.");
            Pause();
        }

        public string EnterPurpose()
        {
            System.Console.WriteLine("Please enter an optional purpose for the transaction (max. 30 characters):");
            return LimitChars(30);
        }

        public void TransactionSuccess(string name, string transactionString)
        {
            System.Console.WriteLine("\nThis transaction was added:");
            System.Console.WriteLine(name + "\t" + transactionString);
            Pause();
        }

        public string AddColleague()
        {
            System.Console.Write("Please enter the name of the colleague you want to add:\n");
            System.Console.Write("(Max. 20 characters)\n");
            return LimitChars(20);
        }

        public void NameIsEmpty()
        {
            System.Console.WriteLine("A name has to have at least one character.");
            Pause();
        }

        public void ColleagueExistsAlready(string name)
        {
            System.Console.WriteLine("\nA colleague named " + name + " exists already.");
            Pause();
        }

        public void AccountSuccess(string name)
        {
            System.Console.WriteLine("\nColleague " + name + " has been added.");
            Pause();
        }

        public string RemoveColleague()
        {
            System.Console.Write("Please enter the name of the colleague you want to remove:\n");
            return System.Console.ReadLine();
        }

        public void AccountRemoveSuccess(string name)
        {
            System.Console.WriteLine("\nColleague " + name + " has been removed.");
            Pause();
        }

        private void Pause()
        {
            System.Console.Write("\nPress Enter to continue ...\n");
            System.Console.ReadLine();
        }

        private string LimitChars(int max)
        {
            string input = System.Console.ReadLine();
            if (input.Length > max)
                input = input.Substring(0, max);
            return input;
        }

        public string ChooseSaveLocation()
        {
            System.Console.WriteLine("Please enter the full path (including filename) you want to save at:");
            return System.Console.ReadLine();
        }

        public void DirectoryNotExist()
        {
            System.Console.WriteLine("\nThis path does not exist.");
            Pause();
        }

        public string FileExistsAlready()
        {
            System.Console.WriteLine("\nA file with that name exists already.");
            System.Console.WriteLine("Do you want to overwrite it? (y or Y to proceed)");
            return System.Console.ReadLine();
        }

        public void FileNotSaved()
        {
            System.Console.WriteLine("\nThe file has not been saved.");
            Pause();
        }

        public void FileSaveSuccess()
        {
            System.Console.WriteLine("\nThe file has been saved successfully.");
            Pause();
        }

        public void FileSaveFailure()
        {
            System.Console.WriteLine("\nThere was an Error when trying to save the file.");
            System.Console.WriteLine("The file has NOT been saved.");
            Pause();
        }

        public string ChooseLoadLocation()
        {
            System.Console.WriteLine("Please enter the full path (including filename) you want to load from:");
            return System.Console.ReadLine();
        }

        public void FileNotExist()
        {
            System.Console.WriteLine("\nThis file does not exist.");
            Pause();
        }

        public void FileLoadFailure()
        {
            System.Console.WriteLine("\nThere was an Error when trying to load the file.");
            Pause();
        }

        public void FileLoadSuccess()
        {
            System.Console.WriteLine("\nThe file has been loaded successfully.");
            Pause();
        }
    }
}
