using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1
{
    public static class ArgumentReader
    {
        public static bool ReadRequired(out string result)
        {
            result = Console.ReadLine();
            if (!string.IsNullOrEmpty(result)) return true;
            Console.WriteLine("Required argument cannot be empty!");
            return false;
        }

        public static void ReadOptional(out string result) => result = Console.ReadLine();

        public static bool ReadPhoneNumber(out string result)
        {
            result = Console.ReadLine();
            var pattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
            if (Regex.IsMatch(result!, pattern)) return true;
            Console.WriteLine("Wrong phone number!");
            return false;
        }
    }
}
