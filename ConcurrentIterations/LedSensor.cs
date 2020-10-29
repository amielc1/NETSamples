using System;
using System.Threading;

namespace ConcurrentIterations
{

    public interface ISensor
    {
        bool Blink(int color);
    }
    public class LedSensor : ISensor
    {

        public bool Blink(int color)
        {
            Console.WriteLine($"{DateTime.UtcNow.TimeOfDay}  t:{Thread.CurrentThread.ManagedThreadId}  {nameof(LedSensor)} {nameof(Blink)} {color} start");
            Thread.Sleep(color *1000);
            Console.WriteLine($"{DateTime.UtcNow.TimeOfDay} t:{Thread.CurrentThread.ManagedThreadId}  {nameof(LedSensor)} {nameof(Blink)} {color} end");
            return true;
        }

    }
}
