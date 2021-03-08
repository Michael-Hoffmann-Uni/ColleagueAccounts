using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ColleagueAccounts.Tests
{
    [TestClass]
    public class AccountImplementationTest
    {
        [TestMethod]
        public void TestAddTransaction()
        {
            Controller controller = new Controller();
            IAccount account = new AccountImplementation("Hans Müller");
            controller.AccountManager.AddAccount(account);

            decimal value = 10.2m;
            DateTime date = Convert.ToDateTime("10.12.2001");
            string purpose = "This is a purpose.";
            ITransaction transaction = new TransactionImplementation(value, date, purpose);
            ITransaction referenceTransaction = new TransactionImplementation(value, date, purpose);

            account.AddTransaction(transaction);
            // Checks for the number of transactions in the account.
            Assert.AreEqual(1, account.TransactionList.Count);
            // Checks if the transaction is present in the account.
            Assert.AreEqual(referenceTransaction, account.TransactionList[0]);
        }
    }
}
