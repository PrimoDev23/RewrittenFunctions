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
        public static bool EndsWith(this string input, string end)
        {
            if (end.Length > input.Length)
            {
                throw new ArgumentException("The end-string can't be longer than the string to check");
            }
            return EndsWithFull(input, end);
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

        public static bool StartsWith(string input, string start)
        {
            if (start.Length > input.Length)
            {
                throw new ArgumentException("The start-string can't be longer than the string to check");
            }

            for (int i = 0; i < start.Length; i++)
            {
                if (input[i] != start[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
