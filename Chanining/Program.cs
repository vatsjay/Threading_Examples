using System;

namespace Chanining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var value = new Calculator(100).Add(10).Sub(32).Add(10);

            Console.WriteLine(value.Result());
            Console.ReadLine();
        }
    }
}
