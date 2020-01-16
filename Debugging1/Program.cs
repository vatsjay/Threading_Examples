using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugging
{
    class Program
    {
        static void Main(string[] args)
        {
            var primes =
                from n in Enumerable.Range(1, 10000000)
                .AsParallel()
                .AsOrdered()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                where IsPrime(n)
                select n;
            foreach (var prime in primes)
                Console.Write(prime + ", ");

        }

        public static bool IsPrime(int numberToTest)
        {
            if (numberToTest == 2) return true;
            if (numberToTest < 2 || (numberToTest & 1) == 0) return false;
            int upperBound = (int)Math.Sqrt(numberToTest);
            for (int i = 3; i < upperBound; i += 2)
            {
                if ((numberToTest % i) == 0) return false;
            }
            // It’s prime!
            return true;
        }

    }
}
