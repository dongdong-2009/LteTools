using Lte.Parameters.Abstract;
using Lte.Parameters.Kpi.Abstract;
using Lte.Parameters.Service.Public;
using Moq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Lte.Parameters.Test.Import
{
    [TestFixture]
    public class DumpFromImportedDataTest
    {
        private readonly Mock<IExcelBtsImportRepository<ImportClass>> importBtsRepository
            = new Mock<IExcelBtsImportRepository<ImportClass>>();

        private readonly Mock<IExcelCellImportRepository<ImportClass>> importCellRepository
            = new Mock<IExcelCellImportRepository<ImportClass>>();

        private readonly Mock<IBtsDumpRepository<ImportClass>> dumpBtsRepository
            = new Mock<IBtsDumpRepository<ImportClass>>();

        private readonly Mock<ICellDumpRepository<ImportClass>> dumpCellRepository
            = new Mock<ICellDumpRepository<ImportClass>>();

        private readonly Mock<IParametersDumpResults> dumpResults
            =new Mock<IParametersDumpResults>();

        [SetUp]
        public void TestInitialize()
        {
            importBtsRepository.SetupGet(x => x.BtsExcelList).Returns((List<ImportClass>)null);
            importCellRepository.SetupGet(x => x.CellExcelList).Returns((List<ImportClass>)null);
            dumpBtsRepository.Setup(x => x.InvokeAction(
                It.IsAny<IExcelBtsImportRepository<ImportClass>>())
                ).Callback(() => { });
            dumpCellRepository.Setup(x => x.InvokeAction(
                It.Is<IExcelCellImportRepository<ImportClass>>(v => v != null))
                ).Callback<IExcelCellImportRepository<ImportClass>>(
                import =>
                {
                    importBtsRepository.SetupGet(x => x.BtsExcelList).Returns(
                    new List<ImportClass> { new ImportClass { Name = "ENodebListSuccess" } });
                    importCellRepository.SetupGet(x => x.CellExcelList).Returns(
                    new List<ImportClass> { new ImportClass { Name = "CellListSuccess" } });
                });

        }

        [Test]
        public void TestDumpFromImportedData_OriginalValues()
        {
            Assert.IsNull(importBtsRepository.Object.BtsExcelList);
            Assert.IsNull(importCellRepository.Object.CellExcelList);
        }

        [Test]
        public void TestDumpFromImportedData_OriginalValues_ImportRepositoryNull()
        {
            ((IExcelBtsImportRepository<ImportClass>)null).DumpFromImportedData(
                dumpBtsRepository.Object, dumpResults.Object);
            Assert.IsNull(importBtsRepository.Object.BtsExcelList);
            ((IExcelCellImportRepository<ImportClass>) null).DumpFromImportedData(
                dumpCellRepository.Object);
            Assert.IsNull(importCellRepository.Object.CellExcelList);
        }

        [Test]
        public void TestDumpFromImportedData_OriginalValues_ImportRepository_NotNull()
        {
            importBtsRepository.Object.DumpFromImportedData(dumpBtsRepository.Object, dumpResults.Object);
            importCellRepository.Object.DumpFromImportedData(dumpCellRepository.Object);
            Assert.IsNotNull(importBtsRepository.Object.BtsExcelList);
            Assert.IsNotNull(importCellRepository.Object.CellExcelList);
            Assert.AreEqual(importBtsRepository.Object.BtsExcelList[0].Name, "ENodebListSuccess");
            Assert.AreEqual(importCellRepository.Object.CellExcelList[0].Name, "CellListSuccess");
        }
    }
}
