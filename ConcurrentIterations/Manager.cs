using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace ConcurrentIterations
{
    public class Manager
    {
        ExternalSensor externalSensor = new ExternalSensor();
        BlockingCollection<int> messages = new BlockingCollection<int>();
        LedSensor ledSensor = new LedSensor();


        public Manager()
        {
            externalSensor.evInvokeSensor += externalSensorHandler;
            externalSensor.Start();
            Task ledSensorTask = Task.Factory.StartNew(() => invokeLedSensor());
        }

        private void invokeLedSensor()
        {
            foreach (var msg in messages.GetConsumingEnumerable())
            {
                ledSensor.Blink(msg);
                Console.WriteLine($"{DateTime.UtcNow.TimeOfDay} t:{Thread.CurrentThread.ManagedThreadId} {nameof(Manager)} {nameof(invokeLedSensor)} foreach {msg}");
            }
        }

        private void externalSensorHandler(object sender, int color)
        {
            messages.Add(color);
            Console.WriteLine($"{DateTime.UtcNow.TimeOfDay} t:{Thread.CurrentThread.ManagedThreadId} {nameof(Manager)} {nameof(externalSensorHandler)} Insert {color} to collection");
        }


    }
}
