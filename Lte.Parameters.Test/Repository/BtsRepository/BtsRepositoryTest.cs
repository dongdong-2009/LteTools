using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Service.Cdma;
using NUnit.Framework;

namespace Lte.Parameters.Test.Repository.BtsRepository
{
    [TestFixture]
    public class BtsRepositoryTest : BtsRepositoryTestConfig
    {
        private BtsExcel btsInfo;

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
            btsInfo = new BtsExcel
            {
                BtsId = 2,
                Name = "First eNodeb",
                DistrictName = "Chancheng",
                TownName = "Qinren",
                Longtitute = 112.3344,
                Lattitute = 23.5566
            };
        }

        [Test]
        public void TestBtsRepository_QueryBtsById()
        {
            CdmaBts bts = repository.Object.Btss.FirstOrDefault(x => x.BtsId == 1);
            Assert.IsNotNull(bts);
            Assert.AreEqual(bts.Name, "FoshanZhaoming");
            Assert.AreEqual(bts.TownId, 122);
            Assert.AreEqual(bts.Longtitute, 112.9987);
            Assert.AreEqual(bts.Lattitute, 23.1233);
        }

        [Test]
        public void TestBtsRepository_QueryBtsByTownIdAndName()
        {
            QueryBtsService byNameBtsService
                = new ByTownIdAndNameQueryBtsService(repository.Object, 122, "FoshanZhaoming");
            CdmaBts bts = byNameBtsService.QueryBts();
            Assert.IsNotNull(bts);
            Assert.AreEqual(bts.BtsId, 1);
            Assert.AreEqual(bts.Longtitute, 112.9987);
            Assert.AreEqual(bts.Lattitute, 23.1233);
        }

        [Test]
        public void TestBtsRepository_SaveBts_AddNewOne_TownExists()
        {
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.IsTrue(SaveOneBts(btsInfo));
            Assert.AreEqual(repository.Object.Btss.Count(), 2);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).TownId, 122);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).Longtitute, 112.3344);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).Lattitute, 23.5566);
        }

        [Test]
        public void TestBtsRepository_SaveBts_AddNewOne_TownExists_UpdateLteInfo()
        {
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.IsTrue(SaveOneBts(btsInfo));
            Assert.AreEqual(repository.Object.Btss.Count(), 2);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).TownId, 122);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).Longtitute, 112.3344);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).Lattitute, 23.5566);
        }

        [Test]
        public void TestBtsRepository_SaveBts_AddNewOne_TownNotExists()
        {
            btsInfo.DistrictName = "Guangzhou";
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.IsTrue(SaveOneBts(btsInfo));
            Assert.AreEqual(repository.Object.Btss.Count(), 2);
            Assert.AreEqual(repository.Object.Btss.ElementAt(1).TownId, -1);
        }

        [Test]
        public void TestBtsRepository_Update_SameTownAndName_SameId()
        {
            btsInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(btsInfo.BtsId, 2, "Wrong Id");
            btsInfo.BtsId = 1;
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).BtsId, 1);
            Assert.IsFalse(SaveOneBts(btsInfo));
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).BtsId, 1);
        }

        [Test]
        public void TestBtsRepository_Update_SameTownAndName_DifferentId()
        {
            btsInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(btsInfo.BtsId, 2);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).BtsId, 1);
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.IsFalse(SaveOneBts(btsInfo));
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.AreEqual(repository.Object.Btss.ElementAt(0).BtsId, 1);
        }

        [Test]
        public void TestBtsRepository_Update_DifferentTownOrName_SameId()
        {
            Assert.AreEqual(btsInfo.BtsId, 2);
            btsInfo.BtsId = 1;
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.IsFalse(SaveOneBts(btsInfo));
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
        }

        [Test]
        public void TestBtsRepository_DeleteBts_ByBtsId()
        {
            DeleteOneBtsService deleteOneBtsService = new DeleteOneBtsService(repository.Object, 1);
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            Assert.IsTrue(deleteOneBtsService.Delete());
            Assert.AreEqual(repository.Object.Btss.Count(), 0);
            repository.MockBtsRepositoryDeleteBts();
            Assert.IsFalse(deleteOneBtsService.Delete());
        }

        [Test]
        public void TestBtsRepository_DeleteBts_ByTownAndName()
        {
            Assert.AreEqual(repository.Object.Btss.Count(), 1);
            DeleteOneBtsService deleteOneBtsService = new DeleteOneBtsService(repository.Object,
                townRepository.Object, "Chancheng", "Qinren", "FoshanZhaoming");
            Assert.IsTrue(deleteOneBtsService.Delete());
            Assert.AreEqual(repository.Object.Btss.Count(), 0);
            deleteOneBtsService = new DeleteOneBtsService(repository.Object, 1);
            Assert.IsFalse(deleteOneBtsService.Delete());
        }
    }
}
