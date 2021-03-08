using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColleagueAccounts.Tests
{
    [TestClass]
    public class AccountManagerTest
    {
        [TestMethod]
        public void TestAddAccount()
        {
            Controller controller = new Controller();
            string name = "Peter Schmidt";
            IAccount account = new AccountImplementation(name);
            IAccount referenceAccount = new AccountImplementation(name);
            controller.AccountManager.AddAccount(account);

            // Checks for the number of accounts in the account-manager.
            Assert.AreEqual(1, controller.AccountManager.AccountList.Count);
            // Checks if the account is present in the account-manager.
            Assert.AreEqual(referenceAccount, controller.AccountManager.AccountList[0]);
        }

        [TestMethod]
        public void TestRemoveAccount()
        {
            Controller controller = new Controller();
            string name = "Peter Schmidt";
            string name2 = "Max Mustermann";
            IAccount account = new AccountImplementation(name);
            IAccount account2 = new AccountImplementation(name2);
            controller.AccountManager.AccountList.Add(account);
            controller.AccountManager.RemoveAccount(name);

            // Checks if at least one account gets removed from the account-manager.
            Assert.AreEqual(0, controller.AccountManager.AccountList.Count);

            controller.AccountManager.AccountList.Add(account);
            controller.AccountManager.AccountList.Add(account2);
            controller.AccountManager.RemoveAccount(name2);

            // Checks if exactly one account gets removed from the account manager.
            Assert.AreEqual(1, controller.AccountManager.AccountList.Count);
            // Checks if the account that was not supposed to be removed is still present.
            Assert.AreEqual(account, controller.AccountManager.AccountList[0]);
        }
    }
}
