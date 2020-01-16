using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool
{
    public static class Program2
    {
        private const string URL = "https://docs.microsoft.com/en-us/dotnet/csharp/csharp";

        static void Main4(string[] args)
        {
            DoSynchronousWork();
            var someTask = DoSomethingAsync();
            DoSynchronousWorkAfterAwait();
            someTask.Wait(); //this is a blocking call, use it only on Main method
            Console.ReadLine();
        }
        public static void DoSynchronousWork()
        {
            // You can do whatever work is needed here
            Console.WriteLine("1. Doing some work synchronously");
        }

        static async Task DoSomethingAsync() //A Task return type will eventually yield a void
        {
            Console.WriteLine("2. Async task has started...");
            //GetStringAsync(); // we are awaiting the Async Method GetStringAsync
             await GetStringAsync(); // we are awaiting the Async Method GetStringAsync
            Console.WriteLine("DoSomethingAsync end here");
        }

        static async Task GetStringAsync()
        {
            using (var httpClient = new HttpClient())
            {
                Console.WriteLine("3. Awaiting the result of GetStringAsync of Http Client...");
                //string result = httpClient.GetStringAsync(URL).Result; //execution pauses here while awaiting GetStringAsync to complete
                string result = await httpClient.GetStringAsync(URL); //execution pauses here while awaiting GetStringAsync to complete

                //From this line and below, the execution will resume once the above awaitable is done
                //using await keyword, it will do the magic of unwrapping the Task<string> into string (result variable)
                Console.WriteLine("4. The awaited task has completed. Let's get the content length...");
                Console.WriteLine($"5. The length of http Get for {URL}");
                Console.WriteLine($"6. {result.Length} character");
            }
        }

        static void DoSynchronousWorkAfterAwait()
        {
            //This is the work we can do while waiting for the awaited Async Task to complete
            Console.WriteLine("7. While waiting for the async task to finish, we can do some unrelated work...");
            for (var i = 0; i <= 5; i++)
            {
                for (var j = i; j <= 5; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }



    
    
    
    
    
    
    
    
    
    
    
    
    
    
    class Program
    {
        static void Main4(string[] args)
        {
            var totalAfterTax = SS(); // CalculateTotalAfterTaxAsync(70);
            DoSomethingSynchronous();

            totalAfterTax.Wait();
            Console.ReadLine();
        }

        private static void DoSomethingSynchronous()
        {
            Console.WriteLine($"Doing some synchronous work ,{Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task<float> SS()
        {
            Console.WriteLine($"SS ,{Thread.CurrentThread.ManagedThreadId}");

            var x =await CalculateTotalAfterTaxAsync(2.3f);
            Console.WriteLine($"SS1 ,{Thread.CurrentThread.ManagedThreadId}");

            return x;
        }
        static async Task<float> CalculateTotalAfterTaxAsync(float value)
        {
            Console.WriteLine($"Started CPU Bound asynchronous task on a background thread ,{Thread.CurrentThread.ManagedThreadId}");
            var result = await Task.Run(() => value * 1.2f);
            //var result = Task.Run(() => value * 1.2f);
            Console.WriteLine($"Finished Task. Total of ${value} after tax of 20% is ${result} ,{Thread.CurrentThread.ManagedThreadId}");
            return result;
            //return 32.3f;
        }
    }














































    class Program1
    {
        private static int counter;
        static void Main(string[] args)
        {
            string message = "this is test string";
            doTask(message);

            Console.WriteLine($"In main counter value is :{counter}, thread id is {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.ReadLine();
        }

        private static async Task doTask(string message)
        {
           await doOtherTask(message);
            //Console.WriteLine(ReadFile().Result);
            counter++;
            Console.WriteLine($"1 Inside doTask {counter} thread id {Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task<bool> doOtherTask(string message)
        {
            //await Task.Delay(1000);
            //await Task.Delay(1000);
            //await Task.Run(() => printAsync(message));
            //Thread.Sleep(2000);
            //var _httpClient = new HttpClient();
            //await _httpClient.GetStringAsync("https://dotnetfoundation.org");

            counter++;
            await ReadFile();
            //Console.WriteLine($"Inside doOtherTask {counter}, thread id {Thread.CurrentThread.ManagedThreadId}");
            //await printAsync(message);
            Thread.Sleep(2000);
            Console.WriteLine($" 2 Inside doOtherTask {counter}, thread id {Thread.CurrentThread.ManagedThreadId}");
            return true;
        }

        private static async Task<bool> printAsync(string message)
        {
            Thread.CurrentThread.Suspend();


            Console.WriteLine("3.1 printAsync threadId {0}", Thread.CurrentThread.ManagedThreadId);

            counter++;
            //await Task.Delay(1000);
            Console.WriteLine("3 printAsync threadId {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
            return true;
        }
         
        private static async Task<int> ReadFile()
        {
            Console.WriteLine("4.1 ReadFile threadId {0}", Thread.CurrentThread.ManagedThreadId);
            
            var reader = File.OpenText(@"C:\Users\JAY\Desktop\TextDocument\stockist_dev5.txt");
            var fileText = await reader.ReadToEndAsync();
            
            Console.WriteLine("4.2 ReadFile after  threadId {0}", Thread.CurrentThread.ManagedThreadId);
            return fileText.Length;
        }
    }
}
