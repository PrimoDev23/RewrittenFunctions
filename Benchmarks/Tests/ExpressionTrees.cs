#define VARS

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
        private List<object> models = new List<object>();

        private Func<object, string> Getter;
        private Action<object, string> Setter;
        private Func<object, string> FGetter;
        private Action<object, string> FSetter;
        private PropertyInfo info;
        private FieldInfo finfo;

        private Func<ITest, string, string> MethodExp;
        private MethodInfo MethodRef;

        [GlobalSetup]
        public void Setup()
        {
            models.Clear();
            for (int i = 0; i < 100; i++)
            {
                models.Add(new TestModel()
                {
                    TestProperty = "Test",
                    TestField = "Test"
                });
            }

            Getter = RewrittenFunctions.ExpressionTree.GetterSetter<object, string>.GetGetterProperty("TestProperty", models[0].GetType());
            Setter = RewrittenFunctions.ExpressionTree.GetterSetter<object, string>.GetSetterProperty("TestProperty", models[0].GetType());
            FGetter = RewrittenFunctions.ExpressionTree.GetterSetter<object, string>.GetGetterField("TestField", models[0].GetType());
            FSetter = RewrittenFunctions.ExpressionTree.GetterSetter<object, string>.GetSetterField("TestField", models[0].GetType());
            info = models[0].GetType().GetProperty("TestProperty");
            finfo = models[0].GetType().GetField("TestField");

            MethodExp = (Func<ITest, string, string>)RewrittenFunctions.ExpressionTree.DynamicMethods<ITest>.GetMethod("TestMethod");
            MethodRef = typeof(ITest).GetMethod("TestMethod");
        }

#if VARS

        [Benchmark]
        public void PropertyReflectionGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                info.GetValue(models[i]);
            }
        }

        [Benchmark]
        public void PropertyExpTreeGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                Getter(models[i]);
            }
        }

        [Benchmark]
        public void PropertyReflectionSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                info.SetValue(models[i], i.ToString());
            }
        }

        [Benchmark]
        public void PropertyExpTreeSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                Setter(models[i], i.ToString());
            }
        }

        [Benchmark]
        public void FieldReflectionGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                finfo.GetValue(models[i]);
            }
        }

        [Benchmark]
        public void FieldExpTreeGetter()
        {
            for (int i = 0; i < 100; i++)
            {
                FGetter(models[i]);
            }
        }

        [Benchmark]
        public void FieldReflectionSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                finfo.SetValue(models[i], i.ToString());
            }
        }

        [Benchmark]
        public void FieldExpTreeSetter()
        {
            for (int i = 0; i < 100; i++)
            {
                FSetter(models[i], i.ToString());
            }
        }

#endif //VARS

#if METHODS
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
#endif //METHODS
    }
}