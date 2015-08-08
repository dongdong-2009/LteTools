using System;
using Lte.Domain.Geo;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Geo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lte.Domain.Test.Measure.Point
{
    [TestClass]
    public class CalculatePerformanceTest : ImportCellsTest
    {
        [TestMethod]
        public void TestCalculatePerformance_OneCell()
        {
            this.ImportOneCell();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[0]);
            Assert.IsNull(measurablePoint.Result.StrongestInterference);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 100);
        }

        [TestMethod]
        public void TestCalculatePerformance_TwoCells_InOneStation_WithSamePci()
        {
            this.ImportTwoCellsInOneStation();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[1]);
            Assert.AreEqual(measurablePoint.Result.StrongestInterference.Cell.Cell, outdoorCellList[0]);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 23.364763, eps);
        }

        [TestMethod]
        public void TestCalculatePerformance_TwoCells_InOneStation_WithDifferentPcis()
        {
            this.ImportTwoCellsInOneStation_WithDifferentMods();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[1]);
            Assert.IsNull(measurablePoint.Result.StrongestInterference);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -146.877442, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 33.364763, eps);
        }

        [TestMethod]
        public void TestCalculatePerformance_ThreeCells_InOneStation_WithSameMods()
        {
            this.ImportThreeCellsInOneStation();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[2]);
            Assert.AreEqual(measurablePoint.Result.StrongestInterference.Cell.Cell, outdoorCellList[1]);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -106.877442, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(2), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, -113.492713, eps);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -113.492713, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 6.61527, eps);
        }

        protected void ImportThreeCellsInOneStation_AllInterferenceWithDifferentModsFromStrongestCell()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18, 1);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18, 1);
            Mock<IOutdoorCell> outdoorCell3 = new Mock<IOutdoorCell>();
            outdoorCell3.MockOutdoorCell(112, 23, 90, 15.2, 18, 0);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);
            outdoorCellList.Add(outdoorCell3.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [TestMethod]
        public void TestCalculatePerformance_ThreeCells_InOneStation_AllInterferenceWithDifferentMods()
        {
            this.ImportThreeCellsInOneStation_AllInterferenceWithDifferentModsFromStrongestCell();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[2]);
            Assert.IsNull(measurablePoint.Result.StrongestInterference);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -106.877442, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(2), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, -113.492713, eps);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -123.492713, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 16.61527, eps);
        }

        protected void ImportThreeCellsInOneStation_OneInterferenceSameMod_OtherInterferenceDifferentMod()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18, 0);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18, 1);
            Mock<IOutdoorCell> outdoorCell3 = new Mock<IOutdoorCell>();
            outdoorCell3.MockOutdoorCell(112, 23, 90, 15.2, 18, 0);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);
            outdoorCellList.Add(outdoorCell3.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [TestMethod]
        public void TestCalculatePerformance_ThreeCells_InOneStation_OneInterferenceSameMod_OtherDifferentMod()
        {
            this.ImportThreeCellsInOneStation_OneInterferenceSameMod_OtherInterferenceDifferentMod();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[2]);
            Assert.AreEqual(measurablePoint.Result.StrongestInterference.Cell.Cell, outdoorCellList[0]);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -106.877442, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(2), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, -113.512679, eps);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -123.317025, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 16.439583, eps);
        }

        [TestMethod]
        public void TestCalculatePerformance_ThreeCells_DifferentStations_SameMod()
        {
            this.ImportThreeCellsInDifferentStations();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[1]);
            Assert.AreEqual(measurablePoint.Result.StrongestInterference.Cell.Cell, outdoorCellList[2]);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -117.676163, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(2), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, -117.624276, eps);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, Double.MinValue);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -117.624276, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 4.111596, eps);
        }

        protected void ImportThreeCellsInDifferentStations_OneDifferentModInterference_OneSameModInterference()
        {
            Mock<IOutdoorCell> outdoorCell1 = new Mock<IOutdoorCell>();
            outdoorCell1.MockOutdoorCell(112, 23, 0, 15.2, 18, 1);
            Mock<IOutdoorCell> outdoorCell2 = new Mock<IOutdoorCell>();
            outdoorCell2.MockOutdoorCell(112, 23, 45, 15.2, 18, 1);
            Mock<IOutdoorCell> outdoorCell3 = new Mock<IOutdoorCell>();
            outdoorCell3.MockOutdoorCell(111.99, 23, 90, 15.2, 18, 0);
            outdoorCellList.Add(outdoorCell1.Object);
            outdoorCellList.Add(outdoorCell2.Object);
            outdoorCellList.Add(outdoorCell3.Object);

            measurablePoint.ImportCells(outdoorCellList, budgetList, model);
        }

        [TestMethod]
        public void TestCalculatePerformance_ThreeCells_DifferentStations_OneSameModInterferenceAndOneDifferentMod()
        {
            this.ImportThreeCellsInDifferentStations_OneDifferentModInterference_OneSameModInterference();
            measurablePoint.CalculatePerformance(0.1);

            Assert.AreEqual(measurablePoint.Result.StrongestCell.Cell.Cell, outdoorCellList[1]);
            Assert.AreEqual(measurablePoint.Result.StrongestInterference.Cell.Cell, outdoorCellList[0]);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(0), -113.512679, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(1), -117.676163, eps);
            Assert.AreEqual(measurablePoint.ReceivedRsrpAt(2), -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.SameModInterferenceLevel, -136.877442, eps);
            Assert.AreEqual(measurablePoint.Result.DifferentModInterferenceLevel, -117.676163, eps);
            Assert.AreEqual(measurablePoint.Result.TotalInterferencePower, -127.183242, eps);
            Assert.AreEqual(measurablePoint.Result.NominalSinr, 13.670562, eps);
        }
    }    
}
