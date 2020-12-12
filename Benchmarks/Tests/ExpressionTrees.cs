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
        private Func<TestModel, string> FGetter;
        private Action<TestModel, string> FSetter;
        private PropertyInfo info;
        private FieldInfo finfo;

        private Func<ITest, string, string> MethodExp;
        private MethodInfo MethodRef;

        [GlobalSetup]
        public void Setup()
        {
            Getter = RewrittenFunctions.ExpressionTree.GetterSetter<TestModel, string>.GetGetterProperty("TestProperty");
            Setter = RewrittenFunctions.ExpressionTree.GetterSetter<TestModel, string>.GetSetterProperty("TestProperty");
            FGetter = RewrittenFunctions.ExpressionTree.GetterSetter<TestModel, string>.GetGetterField("TestField");
            FSetter = RewrittenFunctions.ExpressionTree.GetterSetter<TestModel, string>.GetSetterField("TestField");
            info = model.GetType().GetProperty("TestProperty");
            finfo = model.GetType().GetField("TestField");

            MethodExp = (Func<ITest, string, string>)RewrittenFunctions.ExpressionTree.DynamicMethods<ITest>.GetMethod("TestMethod");
            MethodRef = typeof(ITest).GetMethod("TestMethod");
        }

        //[Benchmark]
        public void PropertyReflectionGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                info.GetValue(model);
            }
        }

        //[Benchmark]
        public void PropertyExpTreeGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                Getter(model);
            }
        }

        //[Benchmark]
        public void PropertyReflectionSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                info.SetValue(model, i.ToString());
            }
        }

        //[Benchmark]
        public void PropertyExpTreeSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                Setter(model, i.ToString());
            }
        }

        //[Benchmark]
        public void FieldReflectionGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                finfo.GetValue(model);
            }
        }

        //[Benchmark]
        public void FieldExpTreeGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                FGetter(model);
            }
        }

        //[Benchmark]
        public void FieldReflectionSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                finfo.SetValue(model, i.ToString());
            }
        }

        //[Benchmark]
        public void FieldExpTreeSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                FSetter(model, i.ToString());
            }
        }

        [Benchmark]
        public void MethodReflection()
        {
            ITest test = new TestModel()
            {
                TestProperty = "Test"
            };

            for (int i = 0; i < 100; i++)
            {
                MethodRef.Invoke(test, new object[] { i.ToString() });
            }
        }

        [Benchmark]
        public void MethodExpTree()
        {
            ITest test = new TestModel()
            {
                TestProperty = "Test"
            };

            for (int i = 0; i < 100; i++)
            {
                MethodExp(test, i.ToString());
            }
        }
    }
}