using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Geo.Entities;
using Lte.Parameters.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Evaluations.Infrastructure;
using Lte.Domain.Measure;

namespace Lte.Evaluations.Test.Infrastructure
{
    [TestFixture]
    public class EvaluationInfrastructureTest
    {
        private EvaluationInfrastructure infrastructure;
        private List<EvaluationOutdoorCell> cellList = new List<EvaluationOutdoorCell>();
        private const double eps = 1E-6;

        [SetUp]
        public void TestInitialize()
        {
            cellList.Add(new EvaluationOutdoorCell
            {
                Pci = 0,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.001,
                Lattitute = 23.001,
                Azimuth = 60
            });
            cellList.Add(new EvaluationOutdoorCell
            {
                Pci = 1,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.001,
                Lattitute = 23.001,
                Azimuth = 180
            });
            cellList.Add(new EvaluationOutdoorCell 
            {
                Pci = 2,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.001,
                Lattitute = 23.001,
                Azimuth = 300
            });
            cellList.Add(new EvaluationOutdoorCell
            {
                Pci = 3,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.002,
                Lattitute = 23.00,
                Azimuth = 60
            });
            cellList.Add(new EvaluationOutdoorCell 
            {
                Pci = 4,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.002,
                Lattitute = 23.002,
                Azimuth = 180
            });
            cellList.Add(new EvaluationOutdoorCell 
            {
                Pci = 5,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.002,
                Lattitute = 23.002,
                Azimuth = 300
            });
        }

        [Test]
        public void TestEvaluationInfrastructure_DefaultConstructor()
        {
            infrastructure = new EvaluationInfrastructure(
                new GeoPoint(113, 23), new GeoPoint(113.003, 23.003), cellList);
            Assert.IsNotNull(infrastructure);
            Assert.IsNotNull(infrastructure.MeasurePointList);
            Assert.IsNotNull(infrastructure.Region);
            Assert.AreEqual(infrastructure.Region.Length, 49);
            Assert.AreEqual(infrastructure.MeasurePointList.Count(), 49);
            Assert.IsNotNull(infrastructure.Region[5].Result);
            Assert.AreEqual(infrastructure.Region[5].Result.NominalSinr, double.MinValue);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList.Count, 6);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList[0].Cell.Cell.Longtitute, 113.001);
        }

        [Test]
        public void TestEvaluationInfrastructure_DefaultConstructor_CalculatePerformance()
        {
            infrastructure = new EvaluationInfrastructure(
                new GeoPoint(113, 23), new GeoPoint(113.003, 23.003), cellList);
            infrastructure.Region.CalculatePerformance(0.1);
            Assert.AreEqual(infrastructure.Region[5].Result.NominalSinr, 2.930694, eps);
            Assert.AreEqual(infrastructure.Region[7].Result.NominalSinr, 11.183212, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.ElementAt(16).Longtitute, 113.000899, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.ElementAt(16).Lattitute, 23.000899, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.ElementAt(16).Result.NominalSinr, 13.634994, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.ElementAt(24).Longtitute, 113.001349, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.ElementAt(24).Lattitute, 23.001349, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.ElementAt(24).Result.NominalSinr, 20.070773, eps);
            double maxSinr = infrastructure.MeasurePointList.Select(x => x.Result.NominalSinr).Max();
            Assert.AreEqual(maxSinr, 21.477256, eps);
            MeasurePoint point = infrastructure.MeasurePointList.FirstOrDefault(x =>
                x.Result.NominalSinr == maxSinr);
            Assert.AreEqual(point.Longtitute, 113.001799, eps);
            Assert.AreEqual(point.Lattitute, 23.002248, eps);
            Assert.AreEqual(infrastructure.MeasurePointList.Select(x => x.Result.NominalSinr).Min(), 0.314985, eps);
        }

        [Test]
        public void TestEvaluationInfrastructure_CellConstructor()
        {
            infrastructure = new EvaluationInfrastructure();
            infrastructure.ImportCellList(cellList);
            Assert.IsNotNull(infrastructure);
            Assert.IsNotNull(infrastructure.MeasurePointList);
            Assert.IsNotNull(infrastructure.Region);
            Assert.AreEqual(infrastructure.Region.Length, 4684);
            Assert.AreEqual(infrastructure.MeasurePointList.Count(), 4684);
            Assert.IsNotNull(infrastructure.Region[5].Result);
            Assert.AreEqual(infrastructure.Region[5].Result.NominalSinr, double.MinValue);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList.Count, 6);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList[0].Cell.Cell.Longtitute, 113.001);
            Assert.AreEqual(infrastructure.Region.DegreeInterval, 0.00045, eps);
        }

        [Test]
        public void TestEvaluationInfrastructure_CellConstructor_CalculatePerformance()
        {
            infrastructure = new EvaluationInfrastructure();
            infrastructure.ImportCellList(cellList);
            infrastructure.Region.CalculatePerformance(0.1);
            Assert.AreEqual(infrastructure.Region[5].Result.NominalSinr, 1.108589, eps);
            Assert.AreEqual(infrastructure.Region[7].Result.NominalSinr, 1.182691, eps);
            double maxSinr = infrastructure.MeasurePointList.Select(x => x.Result.NominalSinr).Max();
            Assert.AreEqual(maxSinr, 22.075509, eps);
            MeasurePoint point = infrastructure.MeasurePointList.FirstOrDefault(x =>
                x.Result.NominalSinr == maxSinr);
            Assert.AreEqual(point.Longtitute, 113.001738, eps);
            Assert.AreEqual(point.Lattitute, 23.002188, eps);
            Assert.AreEqual(point.Result.StrongestCell.ReceivedRsrp, -69.40903, eps);
            Assert.AreEqual(point.Result.StrongestCell.DistanceInMeter, 35.831961, eps);
            Assert.AreEqual(point.CellRepository.CellList.Count, 6);
            Assert.AreEqual(point.CellRepository.CellList[0].ReceivedRsrp, -69.40903, eps);
           
            Assert.AreEqual(point.CellRepository.CellList[0].PciModx, 2);
            Assert.AreEqual(point.CellRepository.CellList[1].PciModx, 0);
            Assert.AreEqual(point.CellRepository.CellList[2].PciModx, 0);
            Assert.AreEqual(point.CellRepository.CellList[3].PciModx, 1);
            Assert.AreEqual(point.CellRepository.CellList[4].PciModx, 1);
            Assert.AreEqual(point.CellRepository.CellList[5].PciModx, 2);
            Assert.AreEqual(infrastructure.MeasurePointList.Select(x => x.Result.NominalSinr).Min(), -0.134126, eps);
        }
    }
}
