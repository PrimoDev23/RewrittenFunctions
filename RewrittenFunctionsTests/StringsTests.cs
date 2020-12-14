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
    public class StringsTests
    {
        [TestMethod()]
        public void ContainsIgnoreCaseRFTest()
        {
            Assert.IsTrue("IKnowThisIsContaining".ContainsIgnoreCaseRF("cONTAIN"));
            Assert.IsFalse("IKnowThisIsContaining".ContainsIgnoreCaseRF("cOTAIN"));
        }

        [TestMethod()]
        public void ContainsRFTest()
        {
            Assert.IsTrue("IKnowThisIsContaining".ContainsRF("Contain", StringComparison.Ordinal));
            Assert.IsFalse("IKnowThisIsContaining".ContainsRF("cONTAIN", StringComparison.Ordinal));
        }

        [TestMethod()]
        public void EndsWithRFTest()
        {
            Assert.IsTrue("IKnowThisIsEndsWith".EndsWithRF("EndsWith"));

            void run()
            {
                "123456".EndsWithRF("1234567");
            }

            Assert.ThrowsException<ArgumentException>(new Action(run));
        }

        [TestMethod()]
        public void EndsWithIgnoreCaseRFTest()
        {
            Assert.IsTrue("IKnowThisIsEndsWith".EndsWithIgnoreCaseRF("eNdSWiTh"));
        }

        [TestMethod()]
        public void IsFilledRFTest()
        {
            Assert.IsTrue("rtfdhsfghdfhf".IsFilledRF());
            Assert.IsFalse("".IsFilledRF());
            Assert.IsFalse(((string)null).IsFilledRF());
        }

        [TestMethod()]
        public void StartsWithRFTest()
        {
            Assert.IsTrue("IKnowThisStartsWith".StartsWithRF("IKnow"));
            Assert.IsFalse("IKnowThisStartsWith".StartsWithRF("jhgjhf"));

            void run()
            {
                "123456".StartsWithRF("1234567");
            }

            Assert.ThrowsException<ArgumentException>(new Action(run));
        }

        [TestMethod()]
        public void StartsWithIgnoreCaseRFTest()
        {
            Assert.IsTrue("IKnowThisStartsWith".StartsWithIgnoreCaseRF("iKNoW"));
            Assert.IsFalse("IKnowThisStartsWith".StartsWithIgnoreCaseRF("jhgjhf"));

            void run()
            {
                "abcdef".StartsWithIgnoreCaseRF("AbCdEfG");
            }

            Assert.ThrowsException<ArgumentException>(new Action(run));
        }
    }
}