using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.io.Models;

namespace Bio.io.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void EnsureCanCreateInstanceOfUser()
        {
            User user1 = new User();
            Assert.IsNotNull(user1);
        }
    }
}
