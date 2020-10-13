using System;
using System.Threading;

namespace NETSamplesConsole
{
    class Program
    {
        private static int sum;

        static void Main(string[] args)
        {
            var _lk = new object();
            //create thread t1 using anonymous method
            Thread t1 = new Thread(() => {
                for (int i = 0; i < 100000000; i++)
                {
                    //increment sum value
                    lock(_lk)
                    {
                        sum++;
                    }
                   
                }
            });

            //create thread t2 using anonymous method
            Thread t2 = new Thread(() => {
                for (int i = 0; i < 100000000; i++)
                {
                    //increment sum value
                    lock (_lk)
                    {
                        sum++;
                    }
                }
            });


            //start thread t1 and t2
            t1.Start();
            t2.Start();

            //wait for thread t1 and t2 to finish their execution
            t1.Join();
            t2.Join();

            //write final sum on screen
            Console.WriteLine("sum: " + sum);

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}
