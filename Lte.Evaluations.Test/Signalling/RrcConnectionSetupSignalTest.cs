using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Regular;
using Lte.Evaluations.Signalling;

namespace Lte.Evaluations.Test.Signalling
{
    [TestFixture]
    public class RrcConnectionSetupSignalTest
    {
        private string signalString = "6813980a1dce0183c0ba007e131ffa211f0c288d980002e808000960";

        [Test]
        public void TestRrcConnectionSetupSignal_BasicParameters()
        {
            RrcConnectionSetupSignal signal = new RrcConnectionSetupSignal(signalString);
            Assert.AreEqual(signal.RrcTransactionIdentifier, 1);
            Assert.IsNotNull(signal.RadioResourceConfigDedicated);
            Assert.AreEqual(signalString.Substring(2, signalString.Length - 2),
                "13980a1dce0183c0ba007e131ffa211f0c288d980002e808000960");
        }
    }
}
