//#define POW
//#define TRYPARSE
//#define MODLONGINT
#define NUMBERBETWEENZEROAND

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Tests
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net462, baseline: true)]
    [RPlotExporter]
    public class Maths
    {
#if POW

        [Benchmark]
        public void PowNormal()
        {
            for (int i = 0; i < 100; i++)
            {
                Math.Pow(2, 100);
            }
        }

        [Benchmark]
        public void PowRF()
        {
            for (int i = 0; i < 100; i++)
            {
                RewrittenFunctions.Math.PowRF(2, 100);
            }
        }

#endif //POW

#if TRYPARSE

        [Benchmark]
        public void TryParseIntNormal()
        {
            int res = 0;
            for (int i = 0; i < 100; i++)
            {
                int.TryParse("123456789", out res);
            }
        }

        [Benchmark]
        public void tryParseIntRF()
        {
            int res = 0;
            for (int i = 0; i < 100; i++)
            {
                RewrittenFunctions.Math.TryParseToIntRF("123456789", out res);
            }
        }

#endif //TRYPARSE

#if MODLONGINT

        [Benchmark]
        public void BigIntVariant()
        {
            BigInteger res = 0;
            for (int i = 0; i < 100; i++)
            {
                BigInteger big = BigInteger.Parse("123456123456123456123456123456");
                res += big % 6;
            }
        }

        [Benchmark]
        public void RFModLongInt()
        {
            int res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += RewrittenFunctions.Math.ModLongIntRF("123456123456123456123456123456", 6);
            }
        }

#endif //MODLONGINT
        
#if NUMBERBETWEENZEROAND

        [Benchmark]
        public void NumberBetween0And5Normal()
        {
            Random r = new Random();
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                sum += NumberBetween0AndIf(5, r.Next(-5, 10));
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int NumberBetween0AndIf(int end, int value)
        {
            if (value < 0 || value > end)
            {
                return 1;
            }
            return 0;
        }

        [Benchmark]
        public void NumberBetween0And5Bitwise()
        {
            Random r = new Random();
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                sum += RewrittenFunctions.Math.NumberBetween0And(5, r.Next(-5, 10));
            }
        }

#endif //NUMBERBETWEENZEROAND
    }
}