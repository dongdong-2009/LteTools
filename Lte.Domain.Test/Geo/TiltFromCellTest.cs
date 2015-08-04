﻿using System;
using Lte.Domain.Geo.Entities;
using Lte.Domain.Geo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Geo;

namespace Lte.Domain.Test.Geo
{
    [TestClass]
    public class TiltFromCellTest
    {
        StubOutdoorCell high = new StubOutdoorCell(113, 23);
        StubOutdoorCell low = new StubOutdoorCell(113, 23);

        [TestInitialize]
        public void TestInitialize()
        {
            high.Height = 45;
            low.Height = 15;
        }

        [TestMethod]
        public void TestTiltFromCell_near()
        {
            StubGeoPoint p = new StubGeoPoint(113.001, 23.001);
            double t1 = high.TiltFromCell(p);
            double t2 = low.TiltFromCell(p);
            Assert.IsTrue(Math.Abs(t1 - 15.969129) < 1E-6);
            Assert.IsTrue(Math.Abs(t2 - 5.448813) < 1E-6);
        }

        [TestMethod]
        public void TestTiltFromCell_far()
        {
            StubGeoPoint p = new StubGeoPoint(113.002, 23.002);
            double t1 = high.TiltFromCell(p);
            double t2 = low.TiltFromCell(p);
            Assert.IsTrue(Math.Abs(t1 - 8.1426822) < 1E-6);
            Assert.IsTrue(Math.Abs(t2 - 2.7305803) < 1E-6);
        }
    }
}
