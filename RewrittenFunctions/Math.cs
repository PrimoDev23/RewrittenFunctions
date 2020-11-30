using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions
{
    /// <summary>
    /// Adds faster Functions for mathematical Calculations
    /// </summary>
    public static class Math
    {
        #region Methods

        /// <summary>
        /// Convert a binary string to a decimal number
        /// </summary>
        /// <param name="binary">Binary-String</param>
        /// <returns></returns>
        public static int BinaryToDecimal(this string binary)
        {
            int result = 0;

            int currentNumber;

            for (int i = binary.Length; i >= 0; i--)
            {
                currentNumber = binary[i] - '0';
                if (currentNumber < 0 || currentNumber > 1)
                {
                    throw new Exception("The input string is not a valid binary number");
                }

                result += Math.PowRF(currentNumber, binary.Length - i);
            }

            return result;
        }

        /// <summary>
        /// Calculate the acad. faculty of the given value
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static int FacultyRF(this int value)
        {
            int result = 1;
            for (int i = 2; i < value + 1; i++)
            {
                result *= i;
            }
            return result;
        }

        /// <summary>
        /// Modulo calculation for a large integer that would need BigInt-Type
        /// </summary>
        /// <param name="s">Integer-Number</param>
        /// <param name="mod">Modulo number</param>
        /// <returns></returns>
        public static int ModLongIntRF(this string s, int mod)
        {
            int res = 0;
            for (int j = 0; j < s.Length; j++)
            {
                res = (res * 10 + s[j] - '0') % mod;
            }
            return res;
        }

        public static int PowRF(int base_value, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= base_value;
            }
            return result;
        }

        /// <summary>
        /// Fast TryParse for Int32
        /// </summary>
        /// <param name="s">String to parse</param>
        /// <param name="number">Parsed Int32</param>
        /// <returns></returns>
        public static bool TryParseToIntRF(this string s, out Int32 number)
        {
            number = 0;

            if (s == null || s.Length == 0)
            {
                return false;
            }

            int negative = s[0] == 45 ? 1 : 0;

            //This is for obvious overflows
            if (s.Length > (negative == -1 ? 11 : 10))
            {
                return false;
            }

            for (int i = negative; i < s.Length; i++)
            {
                char curr = s[i];
                if (!char.IsDigit(curr))
                {
                    number = 0;
                    return false;
                }

                number *= 10;
                number += curr - '0';

                //For not that obvious overflows
                if (number < 0)
                {
                    number = 0;
                    return false;
                }
            }

            if (negative == -1)
            {
                number *= -1;
            }

            return true;
        }

        #endregion Methods
    }
}