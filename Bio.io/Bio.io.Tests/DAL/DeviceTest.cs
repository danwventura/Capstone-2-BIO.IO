using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.io.Models;

namespace Bio.io.Tests
{
    [TestClass]
    public class DeviceTest
    {
        [TestMethod]
        public void EnsureCanCreateInstanceOfDevice()
        {
            Device device1 = new Device();
            Assert.IsNotNull(device1);
        }
    }
}
