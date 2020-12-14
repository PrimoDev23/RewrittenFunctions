using Microsoft.VisualStudio.TestTools.UnitTesting;
using RewrittenFunctions.ExpressionTree;
using RewrittenFunctionsTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions.Tests
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

                var Getter = GetterSetter<TestModel, string>.GetGetterProperty("TestProperty");
                string value = Getter(model);

                Assert.IsTrue(value == model.TestProperty);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetGetterObjectPropertyTest()
        {
            try
            {
                object model = new TestModel()
                {
                    TestProperty = "Test"
                };

                var Getter = GetterSetter<object, object>.GetGetterProperty("TestProperty", model.GetType());
                string value = Getter(model).ToString();

                Assert.IsTrue(value == "Test");
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

                var Getter = GetterSetter<TestModel, string>.GetGetterField("TestField");
                string value = Getter(model);

                Assert.IsTrue(value == model.TestField);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetGetterObjectFieldTest()
        {
            try
            {
                object model = new TestModel()
                {
                    TestField = "Test"
                };

                var Getter = GetterSetter<object, string>.GetGetterField("TestField", model.GetType());
                string value = Getter(model);

                Assert.IsTrue(value == "Test");
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

                var Setter = GetterSetter<TestModel, string>.GetSetterProperty("TestProperty");
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

                var Setter = GetterSetter<TestModel, string>.GetSetterField("TestField");
                Setter(model, "newValue");

                Assert.IsTrue(model.TestField == "newValue");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetMethodTest()
        {
            try
            {
                ITest model = new TestModel();

                var Method = (Func<ITest, string, string>)DynamicMethods<ITest>.GetMethod("TestMethod");
                string ret = Method(model, "Test");

                Assert.IsTrue(ret == "Test123");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}