using System;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphore_Sync
{
    class Program
    {
        private static Semaphore _pool;

        private static int _padding;
        
        private static int counter = 0;

        private static readonly object lockObject = new object();
        
        public static void Main()
        {
            tryLock();
            #region semaphore

            //_pool = new Semaphore(0, 3);

            /*for (int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Worker));
                t.Start(i);
            }

            Thread.Sleep(500);

            Console.WriteLine("Main thread calls Release(3).");
            _pool.Release(3);*/

            #endregion
            Console.WriteLine("Main thread exits.");
        }

        private static void tryLock()
        {
            //int counter=0;
            var a = Task.Run(() =>
            {
                for (int i = 0; i < 5000; i++)
                {
                    increaseValue();
                }
            });
            var b = Task.Run(() => {
                for (int i = 0; i < 500; i++)
                {
                    increaseValue();
                }
            });
            var c = Task.Run(() => {
                for (int i = 0; i < 50; i++)
                {
                    increaseValue();
                }
            });

            Task.WaitAll(a, b, c);

            Console.WriteLine("value of counter should be 5550 actual value is {0}", counter);
        }

        private static void increaseValue()
        {
            /*lock (lockObject)
            { */
            counter++;
            /*}*/
        }

        private static void Worker(object num)
        {
            Console.WriteLine("Thread {0} begins " +
                "and waits for the semaphore.", num);
            _pool.WaitOne();

            int padding = Interlocked.Add(ref _padding, 100);

            Console.WriteLine("Thread {0} enters the semaphore.", num);

            Thread.Sleep(1000 + padding);

            Console.WriteLine("Thread {0} releases the semaphore.", num);
            Console.WriteLine("Thread {0} previous semaphore count: {1}",
                num, _pool.Release());
        }
    }
}
