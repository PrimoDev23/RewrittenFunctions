using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions
{
    /// <summary>
    /// Adds faster Functions for mathematical Calculations
    /// </summary>
    public static class Math
    {
        public const float E = 2.71828183f;

        public const float PI = 3.14159265f;

        public const float Tau = 6.283185307f;

        #region Methods

        /// <summary>
        /// Convert a binary string to a decimal number
        /// </summary>
        /// <param name="binary">Binary-String</param>
        /// <returns></returns>
        public static int BinaryToDecimal(string binary)
        {
            int result = 0;

            int currentNumber;

            for (int i = binary.Length - 1; i >= 0; i--)
            {
                currentNumber = binary[i] - '0';
                if (currentNumber < 0 || currentNumber > 1)
                {
                    throw new Exception("The input string is not a valid binary number");
                }

                if (currentNumber == 1)
                {
                    result += PowRF(2, binary.Length - (i + 1));
                }
            }

            return result;
        }

        /// <summary>
        /// Calculate the acad. faculty of the given value
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static int FacultyRF(int value)
        {
            //We can't calculate when using negative numbers
            if (value < 0)
            {
                throw new Exception("Can't calculate faculty of negative numbers");
            }

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
        public static int ModLongIntRF(string s, int mod)
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
        public static bool TryParseToIntRF(string s, out Int32 number)
        {
            number = 0;

            if (!(s?.Length > 0))
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

        /// <summary>
        /// Checks if a number is between 0 and another number
        /// </summary>
        /// <param name="end">end number (included)</param>
        /// <param name="value">value to check</param>
        /// <returns>1 if between else 0</returns>
        public static int NumberBetween0And(int end, int value)
        {
            return (value >> 31) + ((end - value) >> 31) + 1;
        }

        public static int GreaterThan(int value, int compareTo)
        {
            return (compareTo - value) >> 31 & 1;
        }

        public static int Min(int x, int y)
        {
            return x ^ ((x ^ y) & -(((x - y) >> 31) + 1));
        }

        public static int Max(int x, int y)
        {
            return y ^ ((x ^ y) & -(((x - y) >> 31) + 1));
        }

        /// <summary>
        /// Get the least common multiple of two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>LCM of A and B</returns>
        public static long GetLCM(long a, long b)
        {
            return a * b / GetGCD(a, b);
        }

        /// <summary>
        /// Get the greatest common divider from two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>GCD of A and B</returns>
        public static long GetGCD(long a, long b)
        {
            //It's not possible if one of the numbers is smaller than one
            if (a < 1 || b < 1)
            {
                throw new ArgumentException("Values can't be less than 1");
            }

            //Euclidian algorithm
            long remainder;
            do
            {
                remainder = a % b;
                a = b;
                b = remainder;
            } while (b != 0); //Return if the remainder is 0

            return a;
        }

        /// <summary>
        /// Get primes in a specific range (2 * n + 1)
        /// </summary>
        /// <param name="n">Range to use for the sieve</param>
        /// <returns>Primes</returns>
        public static long[] SieveOfSundram(long n)
        {
            long[] numbers = new long[n];

            //Init the array
            for (int i = 0; i < n; i++)
            {
                numbers[i] = i;
            }

            long removed = 0;

            long j;
            long index;
            for (long i = 1; i < numbers.Length - 1; i++)
            {
                j = i;
                //Get the number that should be removed
                while ((index = i + j + 2 * i * j) <= numbers.Length - 2)
                {
                    /* 1 + 1 + 2 * 1 * 1 = 4
                     * 1 + 2 + 2 * 1 * 2 = 7
                     * 1 + 3 + 2 * 3 = 10
                     * 1 + 4 + 2 * 4 = 13
                     * and so on 
                     */

                    if (numbers[index] != -1)
                    {
                        removed++;
                    }

                    numbers[index] = -1;
                    j++;
                }
            }

            long[] res = new long[n - removed];

            index = 0;
            for (long i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] != -1)
                {
                    res[index++] = 2 * numbers[i] + 1;
                }
            }

            return res;
        }

        /// <summary>
        /// Compute (a² % m)
        /// </summary>
        /// <param name="a">The base value</param>
        /// <param name="exp">The exponent</param>
        /// <param name="mod">The modulus</param>
        /// <returns></returns>
        public static long ExponentMod(long a, long exp, long mod)
        {
            long res = a;
            for (long i = 2; i <= exp; i++)
            {
                //Multiply
                res *= a;

                //And retrieve the modulus
                res %= mod;
            }

            return res;
        }

        /// <summary>
        /// Inverse modulo
        /// </summary>
        /// <param name="a"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long ModInverse(long a, long m)
        {
            if (m == 1) return 0;
            long m0 = m;
            (long x, long y) = (1, 0);

            while (a > 1)
            {
                long q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }

        /// <summary>
        /// Inverse modulo
        /// </summary>
        /// <param name="a"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            if (m == 1) return 0;
            BigInteger m0 = m;
            (BigInteger x, BigInteger y) = (1, 0);

            while (a > 1)
            {
                BigInteger q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }

        #endregion Methods
    }
}