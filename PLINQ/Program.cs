using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace PLINQ
{
    class Program
    {
        static int[] arr = Enumerable.Range(0, 2000).ToArray();
        public static void Main(string[] args)
        {
            Stopwatch watch;

            //for loop
            watch = new Stopwatch();
            watch.Start();
            bool[] results = new bool[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                results[i] = IsPrime(arr[i]);
            }
            watch.Stop();
            Console.WriteLine("For Loop took: " + watch.Elapsed.Seconds);

            //LINQ to Objects
            watch = new Stopwatch();
            watch.Start();
            bool[] results1 = arr.Select(x => IsPrime(x))
                .ToArray();
            watch.Stop();
            Console.WriteLine("LINQ took: " + watch.Elapsed.Seconds);

            //PLINQ
            watch = new Stopwatch();
            watch.Start();
            bool[] results2 = arr.AsParallel().Select(x => IsPrime(x))
                .ToArray();
            watch.Stop();
            Console.WriteLine("PLINQ took: " + watch.Elapsed.Seconds);

            Console.ReadLine();

        }

        static bool IsPrime(int number)
        {
            const int LAST_CANDIDATE = 10000;
            int primes = 0;
            BitArray candidates = new BitArray(LAST_CANDIDATE, true);

            for (int i = 2; i < LAST_CANDIDATE; i++)
            {
                if (candidates[i])
                {
                    for (int j = i * 2; j < LAST_CANDIDATE; j += i)
                    { candidates[j] = false; }
                }
            }

            for (int i = 1; i < LAST_CANDIDATE; i++)
            {
                if (candidates[i])
                {
                    primes++;
                    if (i == number)
                        return true;
                }
            }
                        
            return false;
        }

    }
}
