using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctionsTests.Models
{
    public class TestModel : ITest
    {
        public string TestProperty { get; set; }
        public string TestField;

        string ITest.TestMethod(string parameter)
        {
            return parameter + "123";
        }
    }
}