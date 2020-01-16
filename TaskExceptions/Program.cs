using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = generateException();

                #region abc
                //var task = catchException();

                Thread.Sleep(1000);
                if (task.IsFaulted)
                {
                    foreach (var item in task.Exception.InnerExceptions)
                    {
                        Console.WriteLine(item.Message);
                    }
                }
                #endregion
            }
            catch (AggregateException excep)
            {
                Console.WriteLine(excep.Message);
            }

            Console.WriteLine("End");
            Console.Read();
        }

        private static Task generateException()
        {
            Task task1 = new Task(() => { throw new CustomException("exception from generateException class"); });
            try
            {
                task1.Start();
                //task1.Wait();
            }
            catch (AggregateException excep)
            {
                Console.WriteLine($"Exception generated {excep.Message}");
            }

            return task1;
        }

        private static void catchException()
        {
            try
            {
                Task[] taskArr = new Task[2];

                taskArr[0] = Task.Run(() => {
                    Console.WriteLine("Inside first task");
                    throw new CustomException("exception generated inside try catch block of first task");
                }); 

                taskArr[1] = Task.Run(() => {
                    Console.WriteLine("Inside second task");
                    throw new CustomException("exception generated inside try catch block of second task");
                });

                //Task.WaitAll(taskArr);

            }
            catch (AggregateException excep)
            {
                foreach (var item in excep.InnerExceptions)
                {
                    Console.WriteLine($"Inside exception block {item.Message}");
                }
            }
        }
    }





    class CustomException : Exception
    {
        public CustomException(string exceptionMessage):base(exceptionMessage)
        {

        }

        public CustomException() : base()
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
