using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Evaluations.Signalling;

namespace Lte.Evaluations.Test.Signalling
{
    [TestClass]
    public class RadioResourceConfigDedicatedTest
    {
        private string signalString = "13980a1dce0183c0ba007e131ffa211f0c288d980002e808000960";

        [TestMethod]
        public void TestRadioResourceConfigDedicated_Switchs()
        {
            RadioResourceConfigDedicated signal = new RadioResourceConfigDedicated(signalString);
            Assert.IsTrue(signal.SrbToAddModListPresent);
            Assert.IsFalse(signal.DrbToAddModListPresent);
            Assert.IsFalse(signal.DrbToReleaseListPresent);
            Assert.IsTrue(signal.MacMainConfigPresent);
            Assert.IsTrue(signal.SpsConfigPresent);
            Assert.IsTrue(signal.PhysicalConfigDedicatedPresent);
            Assert.AreEqual(signal.SrbToAddModListLength, 1);
        }
    }
}
