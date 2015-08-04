using System;
using Lte.Domain.Measure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.TypeDefs;

namespace Lte.Domain.Test.Broadcast
{
    [TestClass]
    public class BroadcastModelSettingTest
    {
        private IBroadcastModel model;

        [TestInitialize]
        public void TestInitialize()
        {
            model = new BroadcastModel();
        }

        [TestMethod]
        public void TestMethod_ModelIsNotNull()
        {
            Assert.IsNotNull(model);
            model = new BroadcastModel(FrequencyBandType.Downlink1800, UrbanType.Middle);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void TestMethod_Contruct_Kvalue()
        {
            Assert.AreEqual(model.K1, 69.55);
            Assert.AreEqual(model.K4, 44.9);
            model = new BroadcastModel(utype: UrbanType.Middle);
            Assert.AreNotEqual(model.K1, 85.83);
            Assert.AreNotEqual(model.K4, 60);
            Assert.AreEqual(model.K1, 69.55);
            Assert.AreEqual(model.K4, 44.9);
            model = new BroadcastModel(utype: UrbanType.Dense);
            Assert.AreEqual(model.K1, 85.83);
            Assert.AreEqual(model.K4, 60);
        }

        [TestMethod]
        public void TestMethod_Contruct_Frequency()
        {
            Assert.AreEqual(model.Frequency, 2120);
            model = new BroadcastModel(FrequencyBandType.Downlink1800);
            Assert.AreEqual(model.Frequency, 1860);
            model = new BroadcastModel(FrequencyBandType.Uplink2100);
            Assert.AreEqual(model.Frequency, 1930);
            model = new BroadcastModel(FrequencyBandType.Uplink1800);
            Assert.AreEqual(model.Frequency, 1765);
            model = new BroadcastModel(FrequencyBandType.Tdd2600);
            Assert.AreEqual(model.Frequency, 2645);
        }

        [TestMethod]
        public void TestMethod_Construct_Earfcn()
        {
            model = new BroadcastModel(100);
            Assert.AreEqual(model.Frequency, 2120);
            model = new BroadcastModel(1825);
            Assert.AreEqual(model.Frequency, 1867.5);
            model = new BroadcastModel(1750);
            Assert.AreEqual(model.Frequency, 1860);
        }

        [TestMethod]
        public void TestMethod_SetKvalue()
        {
            Assert.AreEqual(model.UrbanType, UrbanType.Large);
            model.SetKvalue(UrbanType.Dense);
            Assert.AreEqual(model.UrbanType, UrbanType.Dense);
            Assert.AreEqual(model.K1, 85.83);
            Assert.AreEqual(model.K4, 60);
            model.SetKvalue(UrbanType.Middle);
            Assert.AreEqual(model.UrbanType, UrbanType.Middle);
            Assert.AreEqual(model.K1, 69.55);
            Assert.AreEqual(model.K4, 44.9);
        }

        [TestMethod]
        public void TestMethod_SetFrequency()
        {
            model.SetFrequencyBand(FrequencyBandType.Downlink1800);
            Assert.AreEqual(model.Frequency, 1860);
            model.SetFrequencyBand(FrequencyBandType.Uplink2100);
            Assert.AreEqual(model.Frequency, 1930);
            model.SetFrequencyBand(FrequencyBandType.Tdd2600);
            Assert.AreEqual(model.Frequency, 2645);
        }
    }
}
