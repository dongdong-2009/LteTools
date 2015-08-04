﻿using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Service.Cdma;
using Moq;
using NUnit.Framework;

namespace Lte.Parameters.Test.Repository.CellRepository
{
    [TestFixture]
    public class CdmaCellRepositorySaveCellsTest : CdmaCellRepositoryTestConfig
    {
        private readonly Mock<IBtsRepository> btsRepository = new Mock<IBtsRepository>();
        private List<CdmaCellExcel> cellInfos;

        private void TestInitialize()
        {
            Initialize();
            btsRepository.SetupGet(x => x.Btss).Returns(new List<CdmaBts> 
            {
                new CdmaBts
                {
                    BtsId = 1,
                    Name = "FoshanHuafo",
                    Longtitute = 112.3344,
                    Lattitute = 22.7788
                }
            }.AsQueryable());
            cellInfos = new List<CdmaCellExcel>
            {
                new CdmaCellExcel
                {
                    BtsId = 1,
                    SectorId = 1,
                    IsIndoor = "否  ",
                    Frequency = 283,
                    Height = 40,
                    Azimuth = 35,
                    AntennaGain = 17.5,
                    MTilt = 4,
                    ETilt = 7
                },
                new CdmaCellExcel
                {
                    BtsId = 1,
                    SectorId = 1,
                    IsIndoor = "是",
                    Frequency = 37,
                    Height = 33,
                    Azimuth = 65,
                    AntennaGain = 18.5,
                    MTilt = 8,
                    ETilt = 2
                }
            };
        }

        private int SaveCells()
        {
            SaveCdmaCellInfoListService service = new SimpleSaveCdmaCellInfoListService(
                repository.Object, cellInfos, btsRepository.Object);
            ParametersDumpInfrastructure infrastructure = new ParametersDumpInfrastructure();
            service.Save(infrastructure);
            return infrastructure.CdmaCellsInserted;
        }

        [Test]
        public void TestCdmaCellRepositorySaveCells_FirstCell_BtsExist_CellNotExist()
        {
            TestInitialize();
            Assert.AreEqual(repository.Object.Cells.Count(), 1);
            Assert.AreEqual(repository.Object.Cells.ElementAt(0).Frequency, 64);
            Assert.AreEqual(repository.Object.Cells.ElementAt(0).Frequency1, 283);
            Assert.AreEqual(SaveCells(), 2);
            Assert.AreEqual(repository.Object.Cells.Count(), 2);
            Assert.AreEqual(repository.Object.Cells.ElementAt(0).Frequency, 64);
            Assert.AreEqual(repository.Object.Cells.ElementAt(0).Frequency1, 283);
            Assert.IsTrue(repository.Object.Cells.ElementAt(1).IsOutdoor);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency, 65);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency1, 283);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency2, 37);
        }

        [Test]
        public void TestCdmaCellRepositorySaveCells_FirstCell_BtsNotExist()
        {
            TestInitialize();
            cellInfos[0].BtsId = 2;
            Assert.AreEqual(SaveCells(), 1);
            Assert.AreEqual(repository.Object.Cells.Count(), 2);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency, 1);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency1, 37);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency2, -1);
        }

        [Test]
        public void TestCdmaCellRepositorySaveCells_FirstCell_BtsExist_CellExist()
        {
            TestInitialize();
            cellInfos[0].SectorId = 0;
            Assert.AreEqual(SaveCells(), 1);
            Assert.AreEqual(repository.Object.Cells.Count(), 2);
            Assert.AreEqual(repository.Object.Cells.ElementAt(0).Frequency, 64);
            Assert.AreEqual(repository.Object.Cells.ElementAt(0).Frequency1, 283);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency, 1);
            Assert.AreEqual(repository.Object.Cells.ElementAt(1).Frequency1, 37);
        }
    }
}
