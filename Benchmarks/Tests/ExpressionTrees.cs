using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using RewrittenFunctionsTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Tests
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net462, baseline: true)]
    [RPlotExporter]
    public class ExpressionTrees
    {
        public TestModel model = new TestModel()
        {
            TestField = "Test",
            TestProperty = "Test"
        };

        private Func<TestModel, string> Getter;
        private Action<TestModel, string> Setter;
        private PropertyInfo info;

        [GlobalSetup]
        public void Setup()
        {
            Getter = RewrittenFunctions.ExpressionTrees.GetSet<TestModel, string>.GetGetterProperty("TestProperty");
            Setter = RewrittenFunctions.ExpressionTrees.GetSet<TestModel, string>.GetSetterProperty("TestProperty");
            info = model.GetType().GetProperty("TestProperty");
        }

        [Benchmark]
        public void PropertyReflectionGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                info.GetValue(model);
            }
        }

        [Benchmark]
        public void PropertyExpTreeGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                Getter(model);
            }
        }

        [Benchmark]
        public void PropertyReflectionSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                info.SetValue(model, i.ToString());
            }
        }

        [Benchmark]
        public void PropertyExpTreeSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                Setter(model, i.ToString());
            }
        }
    }
}