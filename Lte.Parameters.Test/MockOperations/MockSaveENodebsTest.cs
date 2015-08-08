using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Parameters.MockOperations;
using Lte.Parameters.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lte.Parameters.Test.MockOperations
{
    [TestClass]
    public class MockSaveENodebsTest : MockENodebTestConfig
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
            eNodebRepository.MockENodebRepositorySaveENodeb();
        }

        [TestMethod]
        public void TestMockAddSuccessiveENodebs()
        {
            eNodebRepository.MockENodebRepositorySaveENodeb();
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            eNodebRepository.Object.Insert(
                new ENodeb { TownId = 5, ENodebId = 11, Name = "E-11" });
            Assert.AreEqual(eNodebRepository.Object.Count(), 8);
            eNodebRepository.Object.Insert(
                new ENodeb { TownId = 4, ENodebId = 12, Name = "E-12" });
            Assert.AreEqual(eNodebRepository.Object.Count(), 9);
        }

        [TestMethod]
        public void TestMockSaveSuccessiveENodebs_TownIdExisted()
        {
            Assert.IsTrue(SaveOneENodeb(
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-1", ENodebId = 101 }));
            Assert.AreEqual(eNodebRepository.Object.Count(), 8);
            Assert.IsTrue(SaveOneENodeb(
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-2", ENodebId = 102 }));
            Assert.AreEqual(eNodebRepository.Object.Count(), 9);
        }

        [TestMethod]
        public void TestMockSaveENodebs_TwoSuccessiveENodebs()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            Assert.AreEqual(SaveENodebs(new List<ENodebExcel>
            {
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-1", ENodebId = 101 },
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-2", ENodebId = 102 }
            }), 2);
            Assert.AreEqual(eNodebRepository.Object.Count(), 9);
        }

        [TestMethod]
        public void TestMockSaveENodebs_ThreeSuccessiveENodebs()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            Assert.AreEqual(SaveENodebs(new List<ENodebExcel>
            {
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-1", ENodebId = 101 },
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-2", ENodebId = 102 },
                new ENodebExcel { CityName = "C-5", DistrictName = "D-5", TownName = "T-5", Name = "E-3", ENodebId = 103 }
            }), 3);
            Assert.AreEqual(eNodebRepository.Object.Count(), 10);
        }
    }
}
