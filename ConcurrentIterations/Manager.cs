using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentIterations
{
   public class Manager
    {
        ExternalSensor externalSensor = new ExternalSensor();
        BlockingCollection<string> messages = new BlockingCollection<string>();
        LedSensor ledSensor = new LedSensor();


        public Manager()
        {
            externalSensor.evInvokeSensor += externalSensorHandler;
            externalSensor.Start();
            Task ledSensorTask = Task.Factory.StartNew(()=>invokeLedSensor());
            
        }

        private void invokeLedSensor()
        {
            foreach (var msg in messages.GetConsumingEnumerable())
            {
                ledSensor.Blink(msg);
            }
        }

        private void externalSensorHandler(object sender, string color)
        {
            messages.Add(color);
            Console.WriteLine($"{DateTime.UtcNow.TimeOfDay} {nameof(Manager)} {nameof(externalSensorHandler)} Insert {color} to collection");
        }


    }
}
