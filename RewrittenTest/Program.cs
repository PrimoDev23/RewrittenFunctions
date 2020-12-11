using RewrittenTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestModel model = new TestModel() { IDK = "TEST" };

            var IDKGetter = RewrittenFunctions.ExpressionTrees.GetSet<TestModel, string>.GetSetter("IDK");

            IDKGetter(model, "IDK");
        }
    }
}