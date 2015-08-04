﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Evaluations.Dingli;

namespace Lte.Evaluations.Test.Dingli
{
    [TestClass]
    public class HugelandRecordTest
    {
        private HugelandRecord record;

        [TestMethod]
        public void TestHugelandRecord()
        {
            record = new HugelandRecord
            {
                PdschRbRate = 785098,
                PuschRbRate = 15786,
                DlThroughputInkbps = 168995.8,
                UlThroughputInkbps = 15786,
                PhyThroughputCode0Inkbps = 195052.9
            };
            Assert.AreEqual(record.DlThroughput, 173051699);
            Assert.AreEqual(record.PhyThroughputCode0, 199734169);
            HugelandRecord newRecord = record.Normalize();
            Assert.AreEqual(newRecord.DlThroughput, 43262924);
            Assert.AreEqual(newRecord.PhyThroughputCode0, 49933542);
        }
    }
}
