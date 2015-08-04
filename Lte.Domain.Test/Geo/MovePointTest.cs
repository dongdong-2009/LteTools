using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Geo.Entities;
using Lte.Domain.Geo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Geo;

namespace Lte.Domain.Test.Geo
{
    [TestClass]
    public class MovePointTest
    {
        [TestMethod]
        public void TestMovePoint_50m_Northeast()
        {
            GeoPoint origin = new GeoPoint(112.1, 23.1);
            IGeoPoint<double> point = origin.Move(50, 45);
            Assert.AreEqual(point.Longtitute, 112.1010860, 1E-6);
            Assert.AreEqual(point.Lattitute, 23.101086, 1E-6);
        }

        [TestMethod]
        public void TestMovePoint_50m_Southeast()
        {
            GeoPoint origin = new GeoPoint(112.1, 23.1);
            IGeoPoint<double> point = origin.Move(50, 135);
            Assert.AreEqual(point.Longtitute, 112.1010860, 1E-6);
            Assert.AreEqual(point.Lattitute, 23.098914, 1E-6);
        }

        [TestMethod]
        public void TestMovePoint_50m_Southwest()
        {
            GeoPoint origin = new GeoPoint(112.1, 23.1);
            IGeoPoint<double> point = origin.Move(50, 225);
            Assert.AreEqual(point.Longtitute, 112.0989140, 1E-6);
            Assert.AreEqual(point.Lattitute, 23.098914, 1E-6);
        }

        [TestMethod]
        public void TestMovePoint_50m_Northwest()
        {
            GeoPoint origin = new GeoPoint(112.1, 23.1);
            IGeoPoint<double> point = origin.Move(50, 315);
            Assert.AreEqual(point.Longtitute, 112.0989140, 1E-6);
            Assert.AreEqual(point.Lattitute, 23.101086, 1E-6);
        }
    }
}
