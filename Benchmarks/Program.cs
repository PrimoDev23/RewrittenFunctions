using BenchmarkDotNet.Running;
using Benchmarks.Tests;
using System;
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
            BenchmarkRunner.Run<ExpressionTrees>();

            Console.ReadKey();
        }
    }
}