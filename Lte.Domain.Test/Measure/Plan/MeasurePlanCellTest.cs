using System;
using Lte.Domain.Geo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Measure;
using Moq;
using Lte.Domain.Geo;

namespace Lte.Domain.Test.Measure.Plan
{
    [TestClass]
    public class MeasurePlanCellTest
    {
        private MeasurePlanCell smpCell;

        [TestInitialize]
        public void TestIntialize()
        {
            FakeMeasurableCell mmCell = new FakeMeasurableCell
            {
                OutdoorCell = new StubOutdoorCell(112, 22, 10),
                PciModx = 2,
                ReceivedRsrp = -10
            };

            smpCell = new MeasurePlanCell(mmCell);
        }

        [TestMethod]
        public void TestGenerateMeasurePlanCell()
        {
            Assert.AreEqual(smpCell.Cell.Azimuth, 10);
            Assert.AreEqual(smpCell.PciModx, 2);
            Assert.AreEqual(smpCell.ReceivePower, 0.1);
        }

        [TestMethod]
        public void TestUpdateRsrp_MeasurePlanCell()
        {
            FakeMeasurableCell mmCell = new FakeMeasurableCell()
            {
                ReceivedRsrp = -10
            };

            smpCell.UpdateRsrpPower(mmCell);
            Assert.AreEqual(smpCell.ReceivePower, 0.2);
        }
    }
}
