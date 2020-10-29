using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentIterations
{
    public class ExternalSensor
    {

        public EventHandler<int> evInvokeSensor = delegate { };
        Random rand = new Random();

        public ExternalSensor()
        {

        }

        public void Start()
        {
            Task invokeSensorInternalTask = Task.Factory.StartNew(() => invokeSensorInternal());
        }


        private void invokeSensorInternal()
        {
            while (true)
            {
                var num = rand.Next(0, 5);
                Thread.Sleep(num * 1000);
                Console.WriteLine($"{DateTime.UtcNow.TimeOfDay}  t:{Thread.CurrentThread.ManagedThreadId} {nameof(ExternalSensor)} Send event {nameof(invokeSensorInternal)} num :{num}");
                evInvokeSensor(this, num);
            }
        }
    }
}
