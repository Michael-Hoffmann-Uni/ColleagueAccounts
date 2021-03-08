using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ColleagueAccounts
{
    // An object of this class verifies that provided data
    // is in the correct format.
    class ValidatorImplementation : IValidator
    {
        // Checks if a string can be converted to a decimal
        public bool MoneyCheck(string amount)
        {
            try
            {
                decimal value = Convert.ToDecimal(amount);
                if (GetDec(value) <= 2)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Checks if a string resembles a valid date in typical german format.
        public bool DateCheck(string dateString)
        {
            DateTime date;
            string[] format = { "dd/MM/yyyy" };
            if (!DateTime.TryParseExact(dateString, format, new CultureInfo("de-DE"), DateTimeStyles.None, out date))
                return false;
            return true;
        }
        // Determines the number of decimal places of a decimal.
        private int GetDec(decimal dec, int i = 0)
        {
            decimal multiDec = (decimal)((double)dec * Math.Pow(10, i));
            if (Math.Round(multiDec) == multiDec)
                return i;
            return GetDec(dec, i + 1);
        }
    }
}
