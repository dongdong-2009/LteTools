using System.Collections.Generic;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Geo.Entities;
using Lte.Domain.Geo.Service;
using Lte.Domain.Measure;
using Moq;
using NUnit.Framework;

namespace Lte.Domain.Test.Measure.Point
{
    [TestFixture]
    public class ImportCellsTest
    {
        protected readonly IList<ILinkBudget<double>> budgetList = new List<ILinkBudget<double>>();
        protected readonly IBroadcastModel model = new BroadcastModel();
        protected readonly IList<IOutdoorCell> outdoorCellList = new List<IOutdoorCell>();
        protected const double eps = 1E-6;
        protected readonly MeasurePoint measurablePoint = new MeasurePoint();

        [SetUp]
        public void TestInitialize()
        {
            StubGeoPoint point0 = new StubGeoPoint(112, 23);
            StubGeoPoint point = new StubGeoPoint(point0, 0.01);
            measurablePoint.Longtitute = point.Longtitute;
            measurablePoint.Lattitute = point.Lattitute;

        }

        [Test]
        public void TestImportCells_OneCell()
        {
            ImportOneCell();

            Assert.AreEqual(measurablePoint.CellRepository.CellList.Count, 1);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Cell.Distance, 1.111949, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Cell.AzimuthAngle, 90);
            Assert.AreEqual(FakeComparableCell.Parse(measurablePoint.CellRepository.CellList[0].Cell).MetricCalculate(), 
                31.612974, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Cell.AzimuthFactor(), 30, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Cell.Cell.Height, 40);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Budget.AntennaGain, 18);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Budget.TransmitPower, 15.2);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].TiltFactor(), 1.259912, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].ReceivedRsrp, -136.877442, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].TiltAngle, 2.939795, eps);
        }

        protected void ImportOneCell()
        {
            Mock<IOutdoorCell> outdoorCell = new Mock<IOutdoorCell>();
            outdoorCell.MockOutdoorCell(112, 23, 0, 15.2, 18);
            outdoorCellList.Add(outdoorCell.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [Test]
        public void TestImportCells_TwoCells_InOneStation()
        {
            ImportTwoCellsInOneStation();

            Assert.AreEqual(measurablePoint.CellRepository.CellList.Count, 2);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].ReceivedRsrp, -113.512679, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].TiltAngle, 2.939795, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].ReceivedRsrp, -136.877442, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].TiltAngle, 2.939795, eps);
        }

        protected void ImportTwoCellsInOneStation()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [Test]
        public void TestImportCells_TwoCells_InOneStation_WithDifferentMods()
        {
            ImportTwoCellsInOneStation_WithDifferentMods();

            Assert.AreEqual(measurablePoint.CellRepository.CellList.Count, 2);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].ReceivedRsrp, -113.512679, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].TiltAngle, 2.939795, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].Cell.PciModx, 1);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].ReceivedRsrp, -136.877442, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].TiltAngle, 2.939795, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].Cell.PciModx, 0);
        }

        protected void ImportTwoCellsInOneStation_WithDifferentMods()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18, 1);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [Test]
        public void TestImportCells_ThreeCells_InOneStation()
        {
            ImportThreeCellsInOneStation();

            Assert.AreEqual(measurablePoint.CellRepository.CellList.Count, 3);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].ReceivedRsrp, -106.877442, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].TiltAngle, 2.939795, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].ReceivedRsrp, -113.512679, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].TiltAngle, 2.939795, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[2].ReceivedRsrp, -136.877442, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[2].TiltAngle, 2.939795, eps);
        }

        protected void ImportThreeCellsInOneStation()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18);
            Mock<IOutdoorCell> outdoorCell3 = new Mock<IOutdoorCell>();
            outdoorCell3.MockOutdoorCell(112, 23, 90, 15.2, 18);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);
            outdoorCellList.Add(outdoorCell3.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [Test]
        public void TestGenerateMeasurableCellList_ThreeCells_TwoInOneStation_OtherInOtherStation()
        {
            ImportThreeCellsInDifferentStations();

            Assert.AreEqual(measurablePoint.CellRepository.CellList.Count, 3);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].ReceivedRsrp, -113.512679, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[0].TiltAngle, 2.939795, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].ReceivedRsrp, -117.676163, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[1].TiltAngle, 3.969564, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[2].ReceivedRsrp, -136.877442, eps);
            Assert.AreEqual(measurablePoint.CellRepository.CellList[2].TiltAngle, 2.939795, eps);
        }

        protected void ImportThreeCellsInDifferentStations()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18);
            Mock<IOutdoorCell> outdoorCell3 = new Mock<IOutdoorCell>();
            outdoorCell3.MockOutdoorCell(111.99, 23, 90, 15.2, 18);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);
            outdoorCellList.Add(outdoorCell3.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }
    }
}
