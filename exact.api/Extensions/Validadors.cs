using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace exact.api.Extensions
{
    public class Validadors
    {
        public static bool IsCPFValid(string cpf)
        {
            //remove all but the numbers
            string trimmed = Regex.Replace(cpf, "[^0-9]", string.Empty);

            //valid cpf must be 11 characters long
            if (trimmed.Length != 11)
                return false;

            var numbers = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numbers[i] = (int)trimmed[i] - '0';
            }

            int sum = 0;

            //multiply the cpf numbers by the cpf mask for the first verificaiton digit
            for (int mult = 10, index = 0; mult > 1; mult--)
            {
                sum += mult * numbers[index];
                index++;
            }

            //calculate the first verification digit
            int rest = sum % 11;
            int result = rest < 2 ? 0 : 11 - rest;

            //check the first verification digit
            if (numbers[9] != result)
                return false;


            sum = 0;
            //multiply the cpf numbers by the cpf mask for the second verificaiton digit
            for (int mult = 11, index = 0; mult > 1; mult--)
            {
                sum += mult * numbers[index];
                index++;
            }

            //check the second verification digit
            rest = sum % 11;
            result = rest < 2 ? 0 : 11 - rest;

            if (numbers[10] != result)
                return false;

            //if we got here everything matches the formula, so the cpf must be correct!
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            return Regex.IsMatch(email, @"^.+@[A-Z0-9.-]+\.[A-Z]{2,}$", RegexOptions.IgnoreCase);
        }
    }
}
