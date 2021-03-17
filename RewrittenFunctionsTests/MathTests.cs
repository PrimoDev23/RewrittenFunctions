using Microsoft.VisualStudio.TestTools.UnitTesting;
using RewrittenFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions.Tests
{
    [TestClass()]
    public class MathTests
    {
        [TestMethod()]
        public void BinaryToDecimalTest()
        {
            string binary = "1001";

            Assert.IsTrue(Math.BinaryToDecimal(binary) == 9);
        }

        [TestMethod()]
        public void FacultyTest()
        {
            Assert.IsTrue(Math.FacultyRF(5) == 120);

            void run()
            {
                Math.FacultyRF(-10);
            }

            Assert.ThrowsException<Exception>(new Action(run));
        }

        [TestMethod()]
        public void ModLongNumberTest()
        {
            Assert.IsTrue(Math.ModLongIntRF("123456123456123456123456", 6) == 0);
        }

        [TestMethod()]
        public void PowTest()
        {
            Assert.IsTrue(Math.PowRF(5, 5) == 3125);
        }

        [TestMethod()]
        public void TryParseTest()
        {
            Math.TryParseToIntRF("123456", out int res);
            Assert.IsTrue(res == 123456);
        }

        [TestMethod()]

        public void GreaterThanTest()
        {
            Assert.AreEqual(Math.GreaterThan(5, 3), 1);
            Assert.AreEqual(Math.GreaterThan(3, 5), 0);
        }
    }
}