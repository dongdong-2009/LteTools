using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Test.Process
{
    [TestClass]
    public class CellProcessRepositoryTest
    {
        private StubCellProcessRepository repository = new StubCellProcessRepository();

        [TestMethod]
        public void TestCellProcessRepository_BasicParameters()
        {
            Assert.AreEqual(repository.Cells.Count(), 1);
            Assert.AreEqual(repository.Cells.ElementAt(0).ENodebId, 1);
            Assert.AreEqual(repository.Cells.ElementAt(0).SectorId, 2);
        }

        [TestMethod]
        public void TestCellProcessRepository_CurrentProgress_0()
        {
            Assert.AreEqual(repository.CurrentProgress, 0);
            Assert.AreEqual(repository.SaveCells(null, null), 0);
            Assert.AreEqual(repository.CurrentProgress, 1);
            Assert.AreEqual(repository.SaveCells(null, null), 1);
            Assert.AreEqual(repository.CurrentProgress, 2);
        }

        [TestMethod]
        public void TestCellProcessRepository_CurrentProgress_5()
        {
            Assert.AreEqual(repository.CurrentProgress, 0);
            repository.AddOneCell(null);
            Assert.AreEqual(repository.SaveCells(null, null), 5);
            Assert.AreEqual(repository.CurrentProgress, 6);
            repository.AddOneCell(null);
            Assert.AreEqual(repository.SaveCells(null, null), 0);
            Assert.AreEqual(repository.CurrentProgress, 1);
        }
    }
}
