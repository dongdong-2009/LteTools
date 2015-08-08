using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Lte.Domain.Geo;
using Lte.Domain.Regular;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class SumOfPowerLevelTest
    {
        private class LevelObject
        {
            public double Value { get; set; }
        }

        private IList<LevelObject> levelList = new List<LevelObject>();
        const double eps = 1E-6;

        [Test]
        public void Test_EmptyList()
        {
            double sum = levelList.SumOfPowerLevel(x => x.Value);
            Assert.AreEqual(sum, Double.MinValue);
        }

        [Test]
        public void Test_OneItem()
        {
            for (int a = 0; a < 10; a++)
            {
                levelList.Clear();
                levelList.Add(new LevelObject() { Value = a });
                double sum = levelList.SumOfPowerLevel(x => x.Value);
                Assert.AreEqual(sum, a, eps);
            }
        }

        [Test]
        public void Test_TwoSameItems()
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
