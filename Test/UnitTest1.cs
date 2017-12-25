using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using Host;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HostStart()
        {
            ServiceHost serviceHost = new ServiceHost(typeof(CalculationService));
            serviceHost.Open();
        }

       
    }
}
