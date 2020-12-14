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

            int dec = Math.BinaryToDecimal(binary);

            Assert.IsTrue(dec == 9);
        }
    }
}