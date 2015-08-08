using System.Linq;
using Lte.Parameters.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Evaluations.Infrastructure;

namespace Lte.Evaluations.Test.Infrastructure
{
    [TestFixture]
    public class EvaluationAddCellTest
    {
        private readonly EvaluationInfrastructure infrastructure
            = new EvaluationInfrastructure();

        [Test]
        public void TestEvaluationAddCell_AddOneCell()
        {
            infrastructure.AddCell(new EvaluationOutdoorCell
            {
                Pci = 0,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825, 
                Longtitute = 113.001,
                Lattitute = 23.001,
                Azimuth = 60
            });
            Assert.IsNotNull(infrastructure);
            Assert.IsNotNull(infrastructure.MeasurePointList);
            Assert.IsNotNull(infrastructure.Region);
            Assert.AreEqual(infrastructure.Region.Length, 4422);
            Assert.AreEqual(infrastructure.MeasurePointList.Count(), 4422);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList.Count, 1);
            infrastructure.AddCell(new EvaluationOutdoorCell
            {
                Pci = 0,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.001,
                Lattitute = 23.001,
                Azimuth = 60
            });
            Assert.IsNotNull(infrastructure.Region);
            Assert.AreEqual(infrastructure.Region.Length, 4422);
            Assert.AreEqual(infrastructure.MeasurePointList.Count(), 4422);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList.Count, 1);
            infrastructure.AddCell(new EvaluationOutdoorCell
            {
                Pci = 0,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.001,
                Lattitute = 23.001,
                Azimuth = 180
            });
            Assert.AreEqual(infrastructure.Region.Length, 4422);
            Assert.AreEqual(infrastructure.MeasurePointList.Count(), 4422);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList.Count, 2);
            infrastructure.AddCell(new EvaluationOutdoorCell
            {
                Pci = 0,
                AntennaGain = 18,
                RsPower = 16.2,
                Frequency = 1825,
                Longtitute = 113.001,
                Lattitute = 23.002,
                Azimuth = 180
            });
            Assert.AreEqual(infrastructure.Region.Length, 4556);
            Assert.AreEqual(infrastructure.MeasurePointList.Count(), 4556);
            Assert.AreEqual(infrastructure.Region[5].CellRepository.CellList.Count, 3);
        }
    }
}
