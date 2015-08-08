using Lte.Evaluations.ViewHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.WebApp.Tests.Parameters
{
    [TestClass]
    public class RegionViewModelMessageTest
    {
        [TestMethod]
        public void TestRegionViewModel_DeleteTownSuccessMessage()
        {
            RegionViewModel viewModel = new RegionViewModel("")
            {
                CityName = "Foshan",
                NewCityName = "Shenzhen",
                DistrictName = "Chancheng",
                NewDistrictName = "Nanhai",
                TownName = "Nanzhuang",
                NewTownName = "Chengqu"
            };
            Assert.AreEqual(viewModel.DeleteSuccessMessage,
                "删除镇街:Foshan-Chancheng-Nanzhuang成功");
        }

        [TestMethod]
        public void TestRegionViewModel_DeleteTownFailMessage()
        {
            RegionViewModel viewModel = new RegionViewModel("")
            {
                CityName = "Foshan",
                NewCityName = "Shenzhen",
                DistrictName = "Chancheng",
                NewDistrictName = "Nanhai",
                TownName = "Nanzhuang",
                NewTownName = "Chengqu"
            };
            Assert.AreEqual(viewModel.DeleteFailMessage,
                "删除镇街:Foshan-Chancheng-Nanzhuang失败。该镇街不存在或镇街下面还带有基站！");
        }
    }
}
