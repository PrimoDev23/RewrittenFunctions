using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions
{
    public static class Strings
    {
        //This makes inining possible
        public static bool EndsWithRF(this string input, string value)
        {
            if (value.Length > input.Length)
            {
                throw new ArgumentException("The value-string can't be longer than the string to check");
            }
            return EndsWithFull(input, value);
        }

        private static bool EndsWithFull(string input, string end)
        {
            int inputLastIndex = input.Length - 1;
            int endLastIndex = end.Length - 1;
            for (int i = 0; i < end.Length; i++)
            {
                if (input[inputLastIndex - i] != end[endLastIndex - i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool StartsWithRF(this string input, string value)
        {
            if (value.Length > input.Length)
            {
                throw new ArgumentException("The value-string can't be longer than the string to check");
            }
            return StartsWithFull(input, value);
        }

        private static bool StartsWithFull(string input, string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (input[i] != value[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsIgnoreCaseRF(this string input, string value)
        {
            return input.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool ContainsIgnoreCaseRF(this string input, string value, StringComparison stringComparison)
        {
            return input.IndexOf(value, stringComparison) >= 0;
        }

        public static bool IsFilledRF(this string input)
        {
            return !(input?.Length > 0);
        }
    }
}
