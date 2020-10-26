using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UsingMoq
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Communication comm = new Communication();
            Mock<Communication> comm_mock = new Mock<Communication>();
            Agent my_agent = new Agent(comm_mock.Object);
            for (int i = 0; i < 10; i++)
            {
                my_agent.SendCommand(DateTime.Now.ToLongTimeString());
            }

            comm_mock.Verify(x => x.Send(It.IsAny<string>()), Times.Exactly(10));
            Assert.AreEqual(5, comm_mock.Object.MesaageSent);

        }
    }
}
