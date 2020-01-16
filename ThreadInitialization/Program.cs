using System;
using System.Threading;

namespace ThreadInitialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Thread nonParameterizedThread = new Thread(print);
            Thread parameterizedThreadWithObj = new Thread(printWithParameter);
            Thread parameterizedThread = new Thread(() => printWithParameter(Thread.CurrentThread.ManagedThreadId));
            
            nonParameterizedThread.Name = "non_paramererized_thread";
            nonParameterizedThread.IsBackground = true;
            nonParameterizedThread.Start();
            parameterizedThreadWithObj.Start("testThread");
            parameterizedThread.Start();

            //nonParameterizedThread.Join();
            //nonParameterizedThread.Abort();

            Console.WriteLine("Back to main");
            Console.ReadLine();
        }

        private static void print()
        {
            Thread.Sleep(2000);
            Console.WriteLine("non parametirized method");
            Console.WriteLine($"Name of the thread is {Thread.CurrentThread.Name}");
        }

        private static void printWithParameter(int threadId)
        {
            Console.WriteLine($"parametirized method thread id {threadId}");
        }
        private static void printWithParameter(Object param)
        {
            Console.WriteLine($"parametirized method thread parameter is {(string)param}");
        }
    }
}