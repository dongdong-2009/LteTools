using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewHelpers;
using Lte.Parameters.MockOperations;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.WebApp.Controllers.Parameters;
using NUnit.Framework;
using Moq;

namespace Lte.WebApp.Tests.ControllerRegion
{
    [TestFixture]
    public class AddTownToEmptySetTest
    {
        private RegionController controller;
        private readonly Mock<ITownRepository> repository = new Mock<ITownRepository>();
        private readonly RegionViewModel viewModel = new RegionViewModel("");
        private readonly IEnumerable<Town> towns = new List<Town>();

        [SetUp]
        public void TestInitialize()
        {
            repository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            repository.MockAddOneTownOperation();
        }

        [Test]
        public void TestAddTown_EmptyInput()
        {
            controller = new RegionController(repository.Object, null, null, null);
            viewModel.CityName = "Foshan";
            viewModel.NewCityName="";
            viewModel.DistrictName = "";
            viewModel.NewDistrictName = "";
            viewModel.TownName = "";
            viewModel.NewTownName = "";
            controller.AddTown(viewModel);
            Assert.AreEqual(controller.TempData["error"], "输入有误！城市、区域、镇区都不能为空。");
        }

        [TestCase("Foshan", "Chancheng", "Chengqu")]
        [TestCase("Fon", "Chancheng", "Chengqu")]
        [TestCase("Foshan", "Chang", "Chengqu")]
        [TestCase("Foshan", "Chancheng", "Cgqu")]
        [TestCase("Foshan2", "Chancheng1", "Chengqu")]
        [TestCase("Foshan", "Chancheng", "Chengqu3")]
        public void TestAddTown_NormalInput(string cityName, string districtName, string townName)
        {
            controller = new RegionController(repository.Object, null, null, null);
            viewModel.CityName = cityName;
            viewModel.NewCityName = "";
            viewModel.DistrictName = "";
            viewModel.NewDistrictName = districtName;
            viewModel.TownName = "";
            viewModel.NewTownName = townName;
            Assert.AreEqual(repository.Object.Towns.Count(), 0);
            controller.AddTown(viewModel);
            IQueryable<Town> resultTowns = repository.Object.Towns;
            Assert.AreEqual(resultTowns.Count(), 1);
            Assert.AreEqual(resultTowns.ElementAt(0).CityName, cityName);
            Assert.AreEqual(resultTowns.ElementAt(0).DistrictName, districtName);
            Assert.AreEqual(resultTowns.ElementAt(0).TownName, townName);
        }
    }
}
