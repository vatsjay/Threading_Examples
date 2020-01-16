using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DataRace
{
    class Program
    {
        public static int counter = 0;
        static void Main(string[] args)
        {
            Object thisLock = new Object();

            Stopwatch watch = new Stopwatch();
            Console.WriteLine("counter started");
            watch.Start();
            Parallel.For(0, 1000, i =>
            {

                Thread.Sleep(1);

                 #region L
                 //Interlocked.Increment(ref counter);
                 lock (thisLock)
                 counter++;
                 #endregion

                 //counter++;
            });

            watch.Stop();
            Console.WriteLine("Seconds Elapsed parallel : " + watch.Elapsed.Seconds);

            /*watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                Thread.Sleep(1);
                counter++;
            }
            watch.Stop();
            Console.WriteLine("Seconds Elapsed normal : " + watch.Elapsed.Seconds);*/
            Console.WriteLine(counter.ToString());
            Console.ReadKey();

        }
    }
}
