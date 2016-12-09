using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.io.Models;

namespace Bio.io.Tests.DAL
{
    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void EnsureCanCreateInstanceOfRoute()
        {
            Route route1 = new Route();
            Assert.IsNotNull(route1);
        }
    }
}
