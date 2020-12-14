using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions
{
    public static class Strings
    {
        #region Methods

        /// <summary>
        /// Check if a string contains an other string (ignoring case)
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <param name="value">Contains-string</param>
        /// <returns></returns>
        public static bool ContainsIgnoreCaseRF(this string input, string value)
        {
            return input.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Check if a string contains an other string
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <param name="value">Contains-string</param>
        /// <param name="stringComparison">Comparison-options</param>
        /// <returns></returns>
        public static bool ContainsRF(this string input, string value, StringComparison stringComparison)
        {
            return input.IndexOf(value, stringComparison) >= 0;
        }

        /// <summary>
        /// Check if a string ends with an other string
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <param name="value">EndsWith-string</param>
        /// <returns></returns>
        public static bool EndsWithRF(this string input, string value)
        {
            if (value.Length > input.Length)
            {
                throw new ArgumentException("The value-string can't be longer than the string to check");
            }
            return EndsWithFull(input, value);
        }

        /// <summary>
        /// Check if a string ends with an other string (ignores casing)
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <param name="value">EndsWith-string</param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCaseRF(this string input, string value)
        {
            if (value.Length > input.Length)
            {
                throw new ArgumentException("The value-string can't be longer than the string to check");
            }
            return EndsWithIgnoreCaseFull(input, value);
        }

        /// <summary>
        /// Check if a string is filled
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <returns></returns>
        public static bool IsFilledRF(this string input)
        {
            return input?.Length > 0;
        }

        /// <summary>
        /// Check if a string starts with an other string
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <param name="value">StartsWith-string</param>
        /// <returns></returns>
        public static bool StartsWithRF(this string input, string value)
        {
            if (value.Length > input.Length)
            {
                throw new ArgumentException("The value-string can't be longer than the string to check");
            }
            return StartsWithFull(input, value);
        }

        /// <summary>
        /// Check if a string starts with an other string (ignores casing)
        /// </summary>
        /// <param name="input">Input-string</param>
        /// <param name="value">StartsWith-string</param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCaseRF(this string input, string value)
        {
            if (value.Length > input.Length)
            {
                throw new ArgumentException("The value-string can't be longer than the string to check");
            }
            return StartsWithIgnoreCaseFull(input, value);
        }

        /// <summary>
        /// Main method for EndsWith
        /// </summary>
        /// <param name="input"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Main method for EndsWith
        /// </summary>
        /// <param name="input"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static bool EndsWithIgnoreCaseFull(string input, string end)
        {
            int inputLastIndex = input.Length - 1;
            int endLastIndex = end.Length - 1;
            for (int i = 0; i < end.Length; i++)
            {
                if (!input[inputLastIndex - i].Equals(end[endLastIndex - i], true))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Main method for StartsWith
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Main method for StartsWithIgnoreCase
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool StartsWithIgnoreCaseFull(string input, string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!input[i].Equals(value[i], true))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool Equals(this char c, char comp, bool ignoreCase)
        {
            //If they are equally in case and same characters we can return true
            if (c == comp)
            {
                return true;
            }
            else if (!ignoreCase) //If they are not equally in case and we don't ignore case return false
            {
                return false;
            }

            //else if we ignore cases let's do it with simple calculations

            //If char is uppercase
            if (c >= 'A' && c <= 'Z')
            {
                return c + 32 == comp;
            }
            else if (c >= 'a' && c <= 'z') //If char is lowercase
            {
                return c - 32 == comp;
            }

            //If it's not a simple letter just match by converting to strings and using equals
            //tred making a CharacterMap but that is a frustrating work
            return c.ToString().Equals(comp.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        #endregion Methods
    }
}