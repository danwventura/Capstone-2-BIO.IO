using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.io.Models;

namespace Bio.io.Tests.DAL
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void EnsureCanCreateInstanceOfImage()
        {
            Image image1 = new Image();
            Assert.IsNotNull(image1);
        }
    }
}
