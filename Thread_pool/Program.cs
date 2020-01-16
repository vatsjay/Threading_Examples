using System;
using System.ComponentModel;
using System.Threading;

namespace Thread_pool
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int abc = 123;
            int p       //Maximum number of worker threads in the pool.
                ,q;     //Maximum number of asynchronous I/O threads in the thread pool.
            ThreadPool.QueueUserWorkItem(printWithParameter, abc);
            ThreadPool.QueueUserWorkItem((a) => { printWithParameter(abc, "secondParameter"); });
            ThreadPool.GetMaxThreads(out p, out q);
            Console.Read();
        }

        private static void printWithParameter(int number, string message)
        {
            Console.WriteLine($"parametirized method number is {number}, message is {message}");
        }

        private static void printWithParameter(object number)
        {
            Console.WriteLine($"parametirized method object is {(int)(number)}");
        }
    }
}
