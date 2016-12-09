using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.io.Models;

namespace Bio.io.Tests
{
    [TestClass]
    public class DataPointTest
    {
        [TestMethod]
        public void EnsureCanCreateInstancOfDataPoint()
        {
            DataPoint datapoint1 = new DataPoint();
            Assert.IsNotNull(datapoint1);
        }
    }
}
