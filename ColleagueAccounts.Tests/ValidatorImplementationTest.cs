using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ColleagueAccounts.Tests
{
    [TestClass]
    public class ValidatorImplementationTest
    {
        [TestMethod]
        public void TestMoneyCheck()
        {
            IValidator validator = new ValidatorImplementation();

            // Checks if input with too many decimals returns false.
            string amount1 = "10,123";
            Assert.AreEqual(false, validator.MoneyCheck(amount1));

            // Checks if input with invalid characters returns false.
            string amount2 = "1d,12";
            Assert.AreEqual(false, validator.MoneyCheck(amount2));

            // Checks if correct input returns true.
            string amount3 = "10,12";
            Assert.AreEqual(true, validator.MoneyCheck(amount3));

            // Checks if negative input returns true.
            string amount4 = "-10,12";
            Assert.AreEqual(true, validator.MoneyCheck(amount4));
        }

        [TestMethod]
        public void TestDateCheck()
        {
            IValidator validator = new ValidatorImplementation();

            // Checks if input of the format d/MM/yyyy returns false.
            string dateString1 = "1.12.2000";
            Assert.AreEqual(false, validator.DateCheck(dateString1));

            // Checks if input of the the format dd/MM/yyyy returns false.
            string dateString2 = "01.1.2000";
            Assert.AreEqual(false, validator.DateCheck(dateString2));

            // Checks if input of the the format dd/MM/yy returns false.
            string dateString3 = "01.1.18";
            Assert.AreEqual(false, validator.DateCheck(dateString3));

            // Checks if input of the the format dd-MM-yyyy returns false.
            string dateString4 = "01-12-2000";
            Assert.AreEqual(false, validator.DateCheck(dateString4));

            // Checks if input with invalid characters returns false.
            string dateString5 = "01.12.y000";
            Assert.AreEqual(false, validator.DateCheck(dateString5));

            // Checks if correct input returns true.
            string dateString6 = "01.12.2008";
            Assert.AreEqual(true, validator.DateCheck(dateString6));
        }
    }
}
