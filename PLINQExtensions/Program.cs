using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace PLINQExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int[] src = Enumerable.Range(0, 200).ToArray();
            var query = src.AsParallel().AsOrdered()
                         .Select(x => ExpensiveFunc(x));

            foreach (var x in query)
            {
                Console.WriteLine(x);
            }
            watch.Stop();
            Console.WriteLine("Elapsed: " + watch.Elapsed.Seconds.ToString());
                      
            Console.ReadLine();
        }

        private static int ExpensiveFunc(int x)
        {
            Thread.Sleep(1);
            return x;
        }

    }
}
