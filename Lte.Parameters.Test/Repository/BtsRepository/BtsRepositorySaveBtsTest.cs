using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Service.Cdma;
using Moq;
using NUnit.Framework;

namespace Lte.Parameters.Test.Repository.BtsRepository
{
    internal class BtsRepositorySaveBtsTestHelper
    {
        private readonly Mock<IBtsRepository> repository;
        private readonly List<BtsExcel> btsInfos;
        private readonly ITownRepository townRepository;

        public BtsRepositorySaveBtsTestHelper(Mock<IBtsRepository> repository,
            List<BtsExcel> btsInfos, ITownRepository townRepository)
        {
            this.repository = repository;
            this.btsInfos = btsInfos;
            this.townRepository = townRepository;
        }

        public void AssertOriginalTest()
        {
            AssertOriginalParameters();
            int expectedSaved = (btsInfos[0].BtsId == repository.Object.Btss.ElementAt(0).BtsId) ? 1 : 2;
            SaveBtsListService service = new ByExcelInfoSaveBtsListService(
                repository.Object, btsInfos, townRepository);
            ParametersDumpInfrastructure infrastructure = new ParametersDumpInfrastructure();
            service.Save(infrastructure, true);
            Assert.AreEqual(repository.Object.Btss.Count(), expectedSaved + 1);

            for (int i = 1; i < repository.Object.Btss.Count(); i++)
            {
                AssertElements(repository.Object.Btss.ElementAt(i), btsInfos[i - expectedSaved + 1]);
            }
        }

        private void AssertElements(CdmaBts bts, BtsExcel btsExcel)
        {
            Assert.AreEqual(bts.TownId,
                GetMatchedTownId(btsExcel, townRepository.Towns.ElementAt(0)));
            Assert.AreEqual(bts.Name, btsExcel.Name);
            Assert.AreEqual(bts.Longtitute, btsExcel.Longtitute);
            Assert.AreEqual(bts.Lattitute, btsExcel.Lattitute);
            Assert.AreEqual(bts.BtsId, btsExcel.BtsId);
        }

        private int GetMatchedTownId(BtsExcel btsExcel, Town town)
        {
            return (btsExcel.DistrictName == town.DistrictName && btsExcel.TownName == town.TownName) ? town.Id : -1;
        }

        public void AssertOriginalParameters()
        {
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).BtsId, 1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).Name, "FoshanZhaoming");
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).ENodebId, -1);
        }

        public void AssertOriginalBtsInfos()
        {
            Assert.AreEqual(btsInfos.Count, 2);
            Assert.AreEqual(btsInfos[0].BtsId, 2, "First bts' id has been modified");
            Assert.AreEqual(btsInfos[1].BtsId, 3);
            Assert.AreEqual(btsInfos[0].Name, "First bts");
            Assert.AreEqual(btsInfos[1].Name, "Second bts");
        }
    }

    [TestFixture]
    public class BtsRepositorySaveBtsTest : BtsRepositoryTestConfig
    {
        private BtsRepositorySaveBtsTestHelper helper;

        [SetUp]
        public void SetUp()
        {
            Initialize();
            townRepository.SetupGet(x => x.Towns).Returns(new List<Town> 
            {
                new Town
                {
                    CityName = "Foshan",
                    DistrictName = "Chancheng",
                    TownName = "Qinren",
                    Id = 122
                }
            }.AsQueryable());
            helper = new BtsRepositorySaveBtsTestHelper(repository, btsInfos, townRepository.Object);
        }

        [Test]
        public void TestBtsRepositorySaveBts_Original()
        {
            helper.AssertOriginalTest();
            Initialize();
        }

        [Test]
        public void TestBtsRepositorySaveBts_Original_LteConsidered()
        {
            helper.AssertOriginalParameters();
            helper.AssertOriginalBtsInfos();
            SaveBtsListService service = new ByExcelInfoSaveBtsListService(
                repository.Object, btsInfos, townRepository.Object, lteRepository.Object);
            ParametersDumpInfrastructure infrastructure = new ParametersDumpInfrastructure();
            service.Save(infrastructure, true);
            Assert.AreEqual(infrastructure.CdmaBtsUpdated, 2, "failure");
            Assert.AreEqual(repository.Object.Btss.Count(), 3);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).ENodebId, -1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).TownId, 122);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).ENodebId, -1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(2).ENodebId, -1);
        }

        [Test]
        public void TestBtsRepositorySaveBts_ModifyFirstItem_TownNotExists()
        {
            string oldDistrict = btsInfos[0].DistrictName;
            btsInfos[0].DistrictName = "Guangzhou";
            helper.AssertOriginalTest();
            btsInfos[0].DistrictName = oldDistrict;
            Initialize();
        }

        [Test]
        public void TestBtsRepositorySaveBts_ModifyFirstItem_SameTownAndName()
        {
            string oldName = btsInfos[0].Name;
            btsInfos[0].Name = "FoshanZhaoming";
            helper.AssertOriginalTest();
            btsInfos[0].Name = oldName;
            Initialize();
        }

        [Test]
        public void TestBtsRepositorySaveBts_ModifyFirstItem_SameTownAndName_SameId()
        {
            int oldId = btsInfos[0].BtsId;
            string oldName = btsInfos[0].Name;
            btsInfos[0].Name = "FoshanZhaoming";
            btsInfos[0].BtsId = 1;
            helper.AssertOriginalTest();
            btsInfos[0].BtsId = oldId;
            btsInfos[0].Name = oldName;
            Initialize();
        }

        [Test]
        public void TestBtsRepositorySaveBts_ModifyFirstItem_DifferentTownOrName_SameId()
        {
            int oldId = btsInfos[0].BtsId;
            btsInfos[0].BtsId = 1;
            helper.AssertOriginalTest();
            btsInfos[0].BtsId = oldId;
            Initialize();
        }
    }
}
