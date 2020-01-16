using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("I am the first task");
            });

            var t2 = t.ContinueWith(delegate
            {
                //simulate compute intensive
                Thread.Sleep(5000);
                return "Devlifestyle";
            });

            //block1
            //string result = t2.Result;
            //Console.WriteLine("result of second task is: " + result);
            //end block1

            //block2
            t2.ContinueWith(delegate
                {
                    Console.WriteLine("Here i am");
                });
            Console.WriteLine("Waiting my task");
            //end block2

            Console.ReadLine();

        }
    }
}
