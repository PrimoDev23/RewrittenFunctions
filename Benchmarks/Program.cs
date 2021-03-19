﻿//#define EXTREE
//#define STRINGS
#define MATHS

using BenchmarkDotNet.Running;
using System;
using Benchmarks.Tests;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
#if EXTREE
            BenchmarkRunner.Run<ExpressionTrees>();
#endif //EXTREE

#if STRINGS
            BenchmarkRunner.Run<Strings>();
#endif //STRINGS

#if MATHS
            BenchmarkRunner.Run<Maths>();
#endif //STRINGS

            Console.ReadKey();
        }
    }
}