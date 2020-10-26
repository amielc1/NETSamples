using System;
using System.Collections.Generic;
using System.Text;

namespace UsingMoq
{

    public class Agent
    {
        ICommunication m_communication;
        public Agent(ICommunication comm)
        {
            m_communication = comm;
        }

        public void SendCommand(string command) => m_communication.Send(command);

    }


    public interface ICommunication
    {
        void Send(string message);
    }


    public class Communication : ICommunication
    {
        static int counter = 0;

        public int MesaageSent { get; private set; }

        public virtual void Send(string message)
        {
            lock (this)
            {
                if (Communication.counter++ % 2 == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Send :  {message}  sentNo {MesaageSent++} countNo {Communication.counter}");
                }
            }
        }
    }


}
