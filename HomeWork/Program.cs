using System;
using System.Threading.Tasks;

namespace HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("");
            });
        }
    }
}
