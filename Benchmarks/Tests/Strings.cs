//#define STARTSWITH
//#define ENDSWITH
#define ISFILLED
//#define CONTAINS

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;
using RewrittenFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Tests
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    [RPlotExporter]
    [InliningDiagnoser(false, true)]
    [DisassemblyDiagnoser(1, true, true)]
    public class Strings
    {
        public string testString = "ThisIsABenchmarkString. It should be long for testing purposes.";

#if STARTSWITH

        [Benchmark]
        public void DefaultStartsWith()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.StartsWith("ThisIsA");
            }
        }

        [Benchmark]
        public void RFStartsWith()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.StartsWithRF("ThisIsA");
            }
        }

        [Benchmark]
        public void RFStartsWithIgnoreCase()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.StartsWithIgnoreCaseRF("ThisIsA");
            }
        }

#endif //STARTSWITH

#if ENDSWITH

        [Benchmark]
        public void DefaultEndsWith()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.EndsWith("purposes.");
            }
        }

        [Benchmark]
        public void RFEndsWith()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.EndsWithRF("purposes.");
            }
        }

        [Benchmark]
        public void RFEndsWithIgnoreCase()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.EndsWithIgnoreCaseRF("purposes.");
            }
        }

#endif //ENDSWITH

#if ISFILLED

        public string empty = "";

        //[Benchmark]
        //public void IsNotNullOrEmpty()
        //{
        //    bool res;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        res = !string.IsNullOrEmpty(empty);
        //    }
        //}

        [Arguments("notEmpty")]
        [Arguments("")]
        [Arguments(null)]
        [Benchmark]
        public void checkManual(string s)
        {
            bool res = s?.Length > 0;
        }

        [Arguments("notEmpty")]
        [Arguments("")]
        [Arguments(null)]
        [Benchmark]
        public void checkFunction(string s)
        {
            bool res = Filled(s);
        }

        public bool Filled(string test)
        {
            return test?.Length > 0;
        }

#endif //ISFILLED

#if CONTAINS

        [Benchmark]
        public void DefaultContainsIgnoreCase()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.ToLower().Contains("should");
            }
        }

        [Benchmark]
        public void RFContainsIgnoreCase()
        {
            bool res = false;
            for (int i = 0; i < 100; i++)
            {
                res = testString.ContainsIgnoreCaseRF("should");
            }
        }

#endif //CONTAINS
    }
}