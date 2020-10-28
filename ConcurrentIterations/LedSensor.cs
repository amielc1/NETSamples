using System;

namespace ConcurrentIterations
{

    public interface ISensor
    {
        bool Blink(string color);
    }
    public class LedSensor : ISensor
    {

        public bool Blink(string color)
        {
            Console.WriteLine($"{DateTime.UtcNow.TimeOfDay} {nameof(LedSensor)} {nameof(Blink)} {color} start");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"{DateTime.UtcNow.TimeOfDay} {nameof(LedSensor)} {nameof(Blink)} {color} end");
            return true;
        }

    }
}
