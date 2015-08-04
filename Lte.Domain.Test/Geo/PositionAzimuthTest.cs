using Lte.Domain.Geo.Entities;
using Lte.Domain.Geo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Geo;

namespace Lte.Domain.Test.Geo
{
    [TestClass]
    public class PositionAzimuthTest
    {
        [TestMethod]
        public void TestPositionAzimuth_SamePoint()
        {
            StubGeoPoint p1 = new StubGeoPoint(113, 23);
            StubGeoPoint p2 = new StubGeoPoint(113, 23);
            Assert.AreEqual(p1.PositionAzimuth(p2), 90);
        }

        [TestMethod]
        public void TestPositionAzimuth_90()
        {
            StubGeoPoint p1 = new StubGeoPoint(113.1, 23);
            StubGeoPoint p2 = new StubGeoPoint(113, 23);
            Assert.AreEqual(p1.PositionAzimuth(p2), 90);
        }

        [TestMethod]
        public void TestPositionAzimuth_0()
        {
            StubGeoPoint p1 = new StubGeoPoint(113, 23.1);
            StubGeoPoint p2 = new StubGeoPoint(113, 23);
            Assert.AreEqual(p1.PositionAzimuth(p2), 0);
        }

        [TestMethod]
        public void TestPositionAzimuth_180()
        {
            StubGeoPoint p1 = new StubGeoPoint(113, 22.9);
            StubGeoPoint p2 = new StubGeoPoint(113, 23);
            Assert.AreEqual(p1.PositionAzimuth(p2), 180);
        }

        [TestMethod]
        public void TestPositionAzimuth_270()
        {
            StubGeoPoint p1 = new StubGeoPoint(112.9, 23);
            StubGeoPoint p2 = new StubGeoPoint(113, 23);
            Assert.AreEqual(p1.PositionAzimuth(p2), 270);
        }

        [TestMethod]
        public void TestPositionAzimuth_45()
        {
            StubGeoPoint p1 = new StubGeoPoint(113.1, 23.1);
            StubGeoPoint p2 = new StubGeoPoint(113, 23);
            double angle = p1.PositionAzimuth(p2);
            Assert.IsTrue(angle > 44.99 && angle < 45.01);
        }
    }
}
