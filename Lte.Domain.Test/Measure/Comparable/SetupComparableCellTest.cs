using Lte.Domain.Geo;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Geo.Service;
using Lte.Domain.Measure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lte.Domain.Test.Measure.Comparable
{
    [TestClass]
    public class SetupComparableCellTest
    {
        ComparableCell mockCC = new ComparableCell();
        Mock<IGeoPoint<double>> mockPoint = new Mock<IGeoPoint<double>>();
        Mock<IOutdoorCell> mockCell = new Mock<IOutdoorCell>();
        const double eps = 1E-6;

        [TestInitialize]
        public void TestInitialize()
        {
            mockPoint.SetupGet(x => x.Longtitute).Returns(113);
            mockPoint.SetupGet(x => x.Lattitute).Returns(23);
            mockCell.SetupGet(x => x.Longtitute).Returns(113.01);
            mockCell.SetupGet(x => x.Lattitute).Returns(23.01);
        }

        [TestMethod]
        public void TestSetupComparableCell_Azimuth_180()
        {
            mockCell.SetupGet(x => x.Azimuth).Returns(180);
            mockCC.SetupComparableCell(mockPoint.Object, mockCell.Object);
            Assert.AreSame(mockCell.Object, mockCC.Cell);
            Assert.AreEqual(mockCC.Distance, mockPoint.Object.SimpleDistance(mockCell.Object));
            Assert.AreEqual(mockCC.AzimuthAngle, 45, eps);
        }

        [TestMethod]
        public void TestSetupComparableCell_Azimuth_OtherAngles()
        {
            mockCell.SetupGet(x => x.Azimuth).Returns(200);
            mockCC.SetupComparableCell(mockPoint.Object, mockCell.Object);
            Assert.AreEqual(mockCC.AzimuthAngle, 25, eps);
            mockCell.SetupGet(x => x.Azimuth).Returns(270);
            mockCC.SetupComparableCell(mockPoint.Object, mockCell.Object);
            Assert.AreEqual(mockCC.AzimuthAngle, -45, eps);
        }
    }
}
