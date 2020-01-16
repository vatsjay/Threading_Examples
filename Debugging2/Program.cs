using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadlocks
{
    class Program
    {
        static void Main(string[] args)
        {
            int transfersCompleted = 0;
            WatchDeadlock.BreakIfRepeats(() => transfersCompleted, 500);
            BankAccount a = new BankAccount { Balance = 1000 };
            BankAccount b = new BankAccount { Balance = 1000 };
            while (true)
            {
                Parallel.Invoke(
                () => Transfer(a, b, 100),
                () => Transfer(b, a, 100));
                transfersCompleted += 2;
            }

        }

        class BankAccount { public int Balance; }
        static void Transfer(BankAccount one, BankAccount two, int amount)
        {
            lock (one)
            {
                lock (two)
                {
                    one.Balance -= amount;
                    two.Balance += amount;
                }
            }
        }

    }
}
