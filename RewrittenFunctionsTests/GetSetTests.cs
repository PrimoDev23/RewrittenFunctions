using Microsoft.VisualStudio.TestTools.UnitTesting;
using RewrittenFunctionsTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions.ExTrees.Tests
{
    [TestClass()]
    public class GetSetTests
    {
        [TestMethod()]
        public void GetGetterPropertyTest()
        {
            try
            {
                TestModel model = new TestModel()
                {
                    TestProperty = "Test"
                };

                var Getter = ExpressionTree<TestModel, string>.GetGetterProperty("TestProperty");
                string value = Getter(model);

                Assert.IsTrue(value == model.TestProperty);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetGetterFieldTest()
        {
            try
            {
                TestModel model = new TestModel()
                {
                    TestField = "Test"
                };

                var Getter = ExpressionTree<TestModel, string>.GetGetterField("TestField");
                string value = Getter(model);

                Assert.IsTrue(value == model.TestField);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetSetterPropertyTest()
        {
            try
            {
                TestModel model = new TestModel()
                {
                    TestProperty = "Test"
                };

                var Setter = ExpressionTree<TestModel, string>.GetSetterProperty("TestProperty");
                Setter(model, "newValue");

                Assert.IsTrue(model.TestProperty == "newValue");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetSetterFieldTest()
        {
            try
            {
                TestModel model = new TestModel()
                {
                    TestField = "Test"
                };

                var Setter = ExpressionTree<TestModel, string>.GetSetterField("TestField");
                Setter(model, "newValue");

                Assert.IsTrue(model.TestField == "newValue");
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}