using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Tests
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net462, baseline: true)]
    [RPlotExporter]
    public class LINQReplacer
    {
        List<int> array;
        IQueryable<int> query;

        [GlobalSetup]
        public void Setup()
        {
            Random r = new Random();
            array = new List<int>(1000);

            for (int i = 0; i < array.Count; i++)
            {
                array[i] = i * r.Next(0, 100);
            }

            query = array.AsQueryable();
        }

        [Benchmark]
        public void IterativeFOD()
        {
            var item = Where();
        }

        private IEnumerable<int> Where()
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] > 100)
                {
                    yield return array[i];
                }
            }
        }

        [Benchmark]
        public void LINQReplacerFOD()
        {
            var item = from i in query
                       where i > 100
                       select i;
        }

        [Benchmark]
        public void LINQFOD()
        {
            var item = array.Where<int>(i => i > 100);
        }
    }
}
