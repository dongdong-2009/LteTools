using System;
using Lte.Domain.Measure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.Domain.Test.Antenna
{
    [TestClass]
    public class VerticalPropertyTest
    {
        [TestMethod]
        public void TestMethod_Default()
        {
            VerticalProperty property = new VerticalProperty();
            Assert.AreEqual(property.CalculateFactor(0), 0);
            Assert.AreEqual(property.CalculateFactor(7), 3);
            Assert.AreEqual(property.CalculateFactor(21), 9);
            Assert.AreEqual(property.CalculateFactor(70), 30);
            Assert.AreEqual(property.CalculateFactor(90), 30);
        }

        [TestMethod]
        public void TestMethod_half10()
        {
            VerticalProperty property = new VerticalProperty(10);
            Assert.AreEqual(property.CalculateFactor(0), 0);
            Assert.AreEqual(property.CalculateFactor(7), 2.1);
            Assert.AreEqual(property.CalculateFactor(10), 3);
            Assert.AreEqual(property.CalculateFactor(30), 9);
            Assert.AreEqual(property.CalculateFactor(90), 27);
        }
    }
}
