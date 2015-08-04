using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Parameters.MockOperations;
using Lte.Parameters.Entities;
using System.Linq;

namespace Lte.Parameters.Test.MockOperations
{
    [TestClass]
    public class MockSaveENodebTest : MockENodebTestConfig
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
            eNodebRepository.MockENodebRepositorySaveENodeb();
        }

        [TestMethod]
        public void TestMockAddOneENodeb()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            eNodebRepository.Object.Insert(
                new ENodeb { TownId = 5, ENodebId = 11, Name = "E-11" });
            Assert.AreEqual(eNodebRepository.Object.Count(), 8);
        }

        [TestMethod]
        public void TestMockSaveENodeb_TownIdExisted()
        {
            Assert.IsTrue(SaveOneENodeb(
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5" }));
            Assert.AreEqual(eNodebRepository.Object.Count(), 8);
            Assert.AreEqual(eNodebRepository.Object.GetAll().ElementAt(7).TownId, 5);
        }

        [TestMethod]
        public void TestMockSaveENodeb_TownIdNotExisted()
        {
            Assert.IsTrue(SaveOneENodeb(
                new ENodebExcel { CityName = "C-6", DistrictName = "D-5", TownName = "T-5" }));
            Assert.AreEqual(eNodebRepository.Object.Count(), 8);
            Assert.AreEqual(eNodebRepository.Object.GetAll().ElementAt(7).TownId, -1);
        }
    }
}
