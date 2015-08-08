using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Lte.Domain.Geo;
using Lte.Domain.Regular;

namespace Lte.Domain.Test.Regular
{
    [TestClass]
    public class SumOfPowerLevelTest
    {
        private class LevelObject
        {
            public double Value { get; set; }
        }

        private IList<LevelObject> levelList = new List<LevelObject>();
        const double eps = 1E-6;

        [TestMethod]
        public void TestMethod_EmptyList()
        {
            double sum = levelList.SumOfPowerLevel(x => x.Value);
            Assert.AreEqual(sum, Double.MinValue);
        }

        [TestMethod]
        public void TestMethod_OneItem()
        {
            for (int a = 0; a < 10; a++)
            {
                levelList.Clear();
                levelList.Add(new LevelObject() { Value = a });
                double sum = levelList.SumOfPowerLevel(x => x.Value);
                Assert.AreEqual(sum, a, eps);
            }
        }

        [TestMethod]
        public void TestMethod_TwoSameItems()
        {
            for (int a = 0; a < 10; a++)
            {
                levelList.Clear();
                levelList.Add(new LevelObject() { Value = a });
                levelList.Add(new LevelObject() { Value = a });
                double sum = levelList.SumOfPowerLevel(x => x.Value);
                Assert.AreEqual(sum, a + 3.0103, eps);
            }
        }
    }
}
