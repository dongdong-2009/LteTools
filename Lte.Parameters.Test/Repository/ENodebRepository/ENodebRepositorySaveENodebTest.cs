using System.Linq;
using NUnit.Framework;

namespace Lte.Parameters.Test.Repository.ENodebRepository
{
    [TestFixture]
    public class ENodebRepositorySaveENodebTest : ENodebRepositoryTestConfig
    {
        [SetUp]
        public void SetUp()
        {
            Initialize();
        }

        [Test]
        public void TestENodebRepository_SaveENodeb_AddNewOne_TownExists()
        {
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.IsTrue(SaveOneENodeb());
            Assert.AreEqual(lteRepository.Object.Count(), 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(1).TownId, 122);
        }

        [Test]
        public void TestENodebRepository_SaveENodeb_AddNewOne_TownNotExists()
        {
            eNodebInfo.CityName = "Guangzhou";
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.IsTrue(SaveOneENodeb());
            Assert.AreEqual(lteRepository.Object.Count(), 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(1).TownId, -1);
        }

        [Test]
        public void TestENodebRepository_Update_SameTownAndName_SameId()
        {
            eNodebInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            eNodebInfo.ENodebId = 1;
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
            Assert.IsTrue(SaveOneENodeb());
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.121");
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
        }

        [Test]
        public void TestENodebRepository_Update_SameTownAndName_DifferentId()
        {
            eNodebInfo.Name = "FoshanZhaoming";
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.IsFalse(SaveOneENodeb());
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).ENodebId, 1);
        }

        [Test]
        public void TestENodebRepository_Update_DifferentTownOrName_SameId()
        {
            Assert.AreEqual(eNodebInfo.ENodebId, 2);
            eNodebInfo.ENodebId = 1;
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.IsFalse(SaveOneENodeb());
            Assert.AreEqual(lteRepository.Object.Count(), 1);
            Assert.AreEqual(lteRepository.Object.GetAll().ElementAt(0).Ip.AddressString, "10.17.165.23");
        }

    }
}
