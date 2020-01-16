using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch;
            watch = new Stopwatch();
            watch.Start();

            //serial implementation
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                //Do stuff
            }
            watch.Stop();
            Console.WriteLine("Serial Time: " + watch.Elapsed.Seconds.ToString());

            //parallel implementation
            watch = new Stopwatch();
            watch.Start();
            System.Threading.Tasks.Parallel.For(0, 10, i =>
            {
                Thread.Sleep(1000);
                //So stuff with i
            });
            watch.Stop();
            Console.WriteLine("Parallel Time: " + watch.Elapsed.Seconds.ToString());

            var a = new List<string> { "abc", "pqr", "xyz" };

            System.Threading.Tasks.Parallel.ForEach(a, (str) =>
            {
                Console.WriteLine(str);
            });

            System.Threading.Tasks.Parallel.Invoke(()=> {
                Console.WriteLine("qwerty");
            },
                () => { Console.WriteLine("asdf"); });

            Console.ReadLine();

        }
    }
}
