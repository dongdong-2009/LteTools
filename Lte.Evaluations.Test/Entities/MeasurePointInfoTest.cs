using Lte.Domain.Geo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Measure;
using Lte.Evaluations.Entities;
using Lte.Domain.Geo;
using Lte.Evaluations.Test.Kml;

namespace Lte.Evaluations.Test.Entities
{
    [TestClass]
    public class MeasurePointInfoTest
    {
        private MeasurePoint _point;
        private StatValueField statValueField = KmlTestInfrastructure.StatValueField;
        private MeasurePointInfo _info;

        [TestInitialize]
        public void TestInitialize()
        {
            
            _point = new MeasurePoint(new GeoPoint(112.1, 23.2))
            {
                Result = new SfMeasurePointResult
                {
                    SameModInterferenceLevel = 0.5,
                    DifferentModInterferenceLevel = 1.5,
                    TotalInterferencePower = 2.5,
                    NominalSinr = 3.5
                }
            };
        }

        [TestMethod]
        public void TestMeasurePointInfo_SameModInterference()
        {
            statValueField.FieldName = "同模干扰电平";
            _info = new MeasurePointInfo(_point, statValueField, 0.1);
            Assert.IsNotNull(_info);
            Assert.AreEqual(_info.ColorStringForKml, "800A0C80");
            Assert.AreEqual(_info.CoordinatesInfo, "112.05,23.15,10 112.15,23.15,10 112.15,23.25,10 112.05,23.25,10");
        }

        [TestMethod]
        public void TestMeasurePointInfo_DiffModInterference()
        {
            statValueField.FieldName = "不同模干扰电平";
            _info = new MeasurePointInfo(_point, statValueField, 0.1);
            Assert.IsNotNull(_info);
            Assert.AreEqual(_info.ColorStringForKml, "80670C0C");
            Assert.AreEqual(_info.CoordinatesInfo, "112.05,23.15,10 112.15,23.15,10 112.15,23.25,10 112.05,23.25,10");
        }

        [TestMethod]
        public void TestMeasurePointInfo_TotalInterference()
        {
            statValueField.FieldName = "总干扰电平";
            _info = new MeasurePointInfo(_point, statValueField, 0.1);
            Assert.IsNotNull(_info);
            Assert.AreEqual(_info.ColorStringForKml, "800A7B0C");
            Assert.AreEqual(_info.CoordinatesInfo, "112.05,23.15,10 112.15,23.15,10 112.15,23.25,10 112.05,23.25,10");
        }

        [TestMethod]
        public void TestMeasurePointInfo_NominalSinr()
        {
            statValueField.FieldName = "标称SINR";
            _info = new MeasurePointInfo(_point, statValueField, 0.1);
            Assert.IsNotNull(_info);
            Assert.AreEqual(_info.ColorStringForKml, "80FFFFFF");
            Assert.AreEqual(_info.CoordinatesInfo, "112.05,23.15,10 112.15,23.15,10 112.15,23.25,10 112.05,23.25,10");
        }
    }
}
