using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RewrittenFunctions.Encryption
{
    public class ECDHS
    {
        public readonly BigInteger pubKey;
        private readonly BigInteger privKey;

        private readonly Curve curve;

        /// <summary>
        /// initialize algorithm with a custom curve
        /// </summary>
        /// <param name="c">Custom curve</param>
        public ECDHS(Curve c)
        {
            curve = c;

            privKey = getRandomBigIntegerFrom(curve.n);

            pubKey = privKey * curve.gx;
        }

        /// <summary>
        /// Initialize algorithm with default curve (secp256k1)
        /// </summary>
        public ECDHS()
        {
            //Use a default curve (secp256k1)
            curve = new Curve("0fffffffffffffffffffffffffffffffffffffffffffffffffffffffefffffc2f", 0, 7, "079be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798", "0fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141", 1);

            privKey = getRandomBigIntegerFrom(curve.n);

            pubKey = privKey * curve.gx;
        }

        /// <summary>
        /// Create the shared key from a second public key
        /// </summary>
        /// <param name="secondKey">Public key of participant 2</param>
        /// <returns>Shared secret</returns>
        public BigInteger CreateSharedSecret(BigInteger secondKey)
        {
            return privKey * secondKey;
        }

        /// <summary>
        /// Get the signature for a message
        /// </summary>
        /// <param name="message">Message to get signature for</param>
        /// <returns>Signature</returns>
        public (string, string) GetSignature(string message)
        {
            //Get the hash of the message
            BigInteger z = getHashFromString(message);

            BigInteger k, r, s;
            do
            {
                do
                {
                    //Select a random integer between 1 and n - 1
                    k = getRandomBigIntegerFrom(curve.n);

                    //Calculate random point
                    r = (k % curve.n) * (curve.gx % curve.n) % curve.n;
                } while (r == 0); //r may not be 0

                //Calculate the signature proof
                s = ((Math.ModInverse(k, curve.n) % curve.n) * ((z % curve.n) + (r % curve.n) * (privKey % curve.n))) % curve.n;
            } while (s == 0); //s also may not be 0

            return (r.ToString(), s.ToString());
        }

        /// <summary>
        /// Check if a sent signature is valid
        /// </summary>
        /// <param name="message">Decrypted message</param>
        /// <param name="pubKey">Public key of sender</param>
        /// <param name="s_r">First part of the signature</param>
        /// <param name="s_s">Second part of the signature</param>
        /// <returns>If signature is valid</returns>
        public bool validateSignature(string message, BigInteger pubKey, string s_r, string s_s)
        {
            BigInteger r = BigInteger.Parse(s_r);
            BigInteger s = BigInteger.Parse(s_s);

            //Check if both ints are inside the range of n
            if(r > 0 && r < curve.n && s > 0 && s < curve.n)
            {
                //Get the according hash for the string
                BigInteger z = getHashFromString(message);

                //Calculate modInverse of s
                BigInteger s1 = Math.ModInverse(s, curve.n);

                //Get point r on curve
                BigInteger r1 = (z * s1 * curve.gx + r * s1 * pubKey) % curve.n;

                //Check if given r and calculated r are the same
                return r1 == r;
            }

            //Covers all cases for an invalid signature
            return false;
        }

        /// <summary>
        /// Create a hash of length n of curve from a message
        /// </summary>
        /// <param name="message">Message to hash</param>
        /// <returns>(Truncated) hash</returns>
        private BigInteger getHashFromString(string message)
        {
            string hash = null;
            using (SHA256 alg = SHA256.Create())
            {
                hash = BitConverter.ToString(alg.ComputeHash(Encoding.UTF8.GetBytes(message))).Replace("-", "");
            }

            BigInteger hashInt = BigInteger.Parse($"0{hash}", NumberStyles.HexNumber);

            int nLength = curve.n.ToString().Length;

            string substring = hashInt.ToString();

            if (substring.Length > nLength)
            {
                substring = hashInt.ToString().Substring(0, nLength);
            }
            return BigInteger.Parse(substring, NumberStyles.HexNumber);
        }

        /// <summary>
        /// Generate a random BigInt by using a byte array
        /// </summary>
        /// <param name="value">Base value</param>
        /// <returns>Random BigInt</returns>
        private BigInteger getRandomBigIntegerFrom(BigInteger value)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] bytes = value.ToByteArray();
            rng.GetBytes(bytes);

            bytes[bytes.Length - 1] = 0;

            return new BigInteger(bytes);
        }
    }

    //Struct defining the curve used for encryption
    public readonly struct Curve
    {
        public readonly BigInteger p;
        public readonly int a;
        public readonly int b;
        public readonly BigInteger gx;
        public readonly BigInteger n;
        public readonly int h;

        public Curve(string p, int a, int b, string gx, string n, int h)
        {
            this.p = BigInteger.Parse(p, NumberStyles.HexNumber);
            this.a = a;
            this.b = b;
            this.gx = BigInteger.Parse(gx, NumberStyles.HexNumber);
            this.n = BigInteger.Parse(n, NumberStyles.HexNumber);
            this.h = h;
        }
    }
}
