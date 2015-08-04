using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Region.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.Parameters.Test.Entities
{
    [TestClass]
    public class CoverageAdjustmentTest
    {
        private void TestAjustFactor(CoverageAdjustment ca, double azimuth, double factor,
            Func<CoverageAdjustment, double> property)
        {
            ca.SetAdjustFactor(azimuth, factor);
            Assert.AreEqual(property(ca), factor);
        }

        [TestMethod]
        public void TestCoverageAdjustment()
        {
            CoverageAdjustment ca = new CoverageAdjustment();
            TestAjustFactor(ca, -180, 9, x => x.Factor165m);
            TestAjustFactor(ca, -164, 9, x => x.Factor165m);
            TestAjustFactor(ca, -149, 9, x => x.Factor135m);
            TestAjustFactor(ca, 35, 9, x => x.Factor45);
        }
    }
}
