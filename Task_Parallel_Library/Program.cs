using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Parallel_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            /* #region comparision

             Stopwatch watch1 = new Stopwatch();
             watch1.Start();
            List<Task> arrTask = new List<Task>();
             Task taskA = Task.Run(() =>
             {
                 for (int i = 0; i < 500; i++)
                 {
                     Console.WriteLine(i);
                 }
             });

             Task taskB = Task.Run(() =>
             {
                 for (int i = 0; i < 500; i++)
                 {
                     Console.WriteLine(i);
                 }
             });
             arrTask.Add(taskA);
             arrTask.Add(taskB);
             taskA.Wait();
             watch1.Stop();


             Stopwatch watch2 = new Stopwatch();
             watch2.Start();
             for (int i = 0; i < 500; i++)
             {
                 Console.WriteLine(i);

             }
             watch2.Stop();

             Console.WriteLine("time elapsed parallel: {0}", watch1.ElapsedMilliseconds);
             Console.WriteLine("time elapsed Normal: {0}", watch2.ElapsedMilliseconds);

             #endregion

             //string a = "parameter";
             //Parallel.Invoke(() => { Console.WriteLine("Inline method"); }, () => print(), () => printMessage(a));

             Task.WaitAll(arrTask.ToArray());
             taskA.Wait();*/

            Task.Run(() => { Console.WriteLine("threadId {0}", Task.CurrentId); })
                .ContinueWith(task => { Console.WriteLine("threadId {0}", Task.CurrentId); }, TaskContinuationOptions.AttachedToParent);

            //showChaining();


            Console.Read();
        }

        private static void print()
        {
            Console.WriteLine("Inside Print");
        }

        private static void printMessage(string message)
        {
            Console.WriteLine("Inside Print {0}", message);
        }

        private static async Task showChaining()
        {
            Task<DayOfWeek> someTask = Task.Run(() => { Console.WriteLine("Inside parent"); return DateTime.Today.DayOfWeek; });
            await someTask.ContinueWith(oldTask => Console.WriteLine("Today is {0}.",oldTask.Result));
        }

       
    }
}
