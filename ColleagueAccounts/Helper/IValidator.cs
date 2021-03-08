using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueAccounts
{
    interface IValidator
    {
        // Validates a string to fit the desired format for a currency.
        public bool MoneyCheck(string value);

        // Validates a string to fit the desired format for a date.
        public bool DateCheck(string dateString);
    }
}
