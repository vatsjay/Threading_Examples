using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

class WatchDeadlock
{
    private static ConcurrentQueue<Timer> _keepAlive = new ConcurrentQueue<Timer>();

    public static void Break() { Debugger.Break(); }

    public static void BreakIn(int milliseconds)
    {
        _keepAlive.Enqueue(new Timer(_ => Debugger.Break(), null, milliseconds, 0));
    }

    public static void BreakIfRepeats<T>(Func<T> valueFactory, int millisecondsPolling) where T : IEquatable<T>
    {
        bool initialized = false;
        T lastValue = default(T);
        _keepAlive.Enqueue(new Timer(_ =>
        {
            T currentValue = valueFactory();
            if (initialized && lastValue.Equals(currentValue)) Debugger.Break();
            initialized = true;
            lastValue = currentValue;
        }, null, millisecondsPolling, millisecondsPolling));
    }
}
