using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Concrete;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Region.Service;
using NUnit.Framework;

namespace Lte.Parameters.Test.Repository.ENodebRepository
{
    [TestFixture]
    public class ENodebRepositoryTestExtended : ENodebRepositoryTestConfig
    {
        private ENodebBaseRepository baseRepository;
        private IEnumerable<Town> towns;

        [SetUp]
        public void SetUp()
        {
            Initialize();
            baseRepository = new ENodebBaseRepository(lteRepository.Object);
            towns = townRepository.Object.Towns.ToList();
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_SaveENodeb_AddNewOne_TownExists()
        {
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsTrue(SaveOneENodeb(baseRepository, townId));
            Assert.AreEqual(lteRepository.Object.Count(), 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(1).TownId, 122);
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_SaveENodeb_AddNewOne_TownNotExists()
        {
            eNodebInfo.CityName = "Guangzhou";
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsTrue(SaveOneENodeb(baseRepository, townId));
            Assert.AreEqual(lteRepository.Object.Count(), 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(1).TownId, -1);
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_NonUpdate_SameTownAndName_SameId()
        {
            eNodebInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            eNodebInfo.ENodebId = 1;
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsFalse(SaveOneENodeb(baseRepository, townId));
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_Update_SameTownAndName_SameId()
        {
            eNodebInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            eNodebInfo.ENodebId = 1;
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsTrue(SaveOneENodeb(baseRepository, townId, true));
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.121");
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_NonUpdate_SameTownAndName_DifferentId()
        {
            eNodebInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsFalse(SaveOneENodeb(baseRepository, townId));
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_Update_SameTownAndName_DifferentId()
        {
            eNodebInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsFalse(SaveOneENodeb(baseRepository, townId, true));
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_NonUpdate_DifferentTownOrName_SameId()
        {
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            eNodebInfo.ENodebId = 1;
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsFalse(SaveOneENodeb(baseRepository, townId));
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
        }

        [Test]
        public void TestENodebRepository_ENodebBaseConsidered_Update_DifferentTownOrName_SameId()
        {
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            eNodebInfo.ENodebId = 1;
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            int townId = towns.QueryId(eNodebInfo);
            Assert.IsFalse(SaveOneENodeb(baseRepository, townId, true));
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
        }
    }
}
