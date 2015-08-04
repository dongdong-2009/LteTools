using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewHelpers;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Region.Service;
using Lte.WebApp.Tests.ControllerParameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lte.WebApp.Tests.Parameters
{
    internal class ENodebQueryViewModelTestHelper
    {
        private readonly IEnumerable<Town> towns;
        private readonly ENodebQueryViewModel viewModel;

        public ENodebQueryViewModelTestHelper(IEnumerable<Town> towns, ENodebQueryViewModel viewModel)
        {
            this.towns = towns;
            this.viewModel = viewModel;
        }

        public void AssertTest(ITownRepository repository)
        {
            viewModel.InitializeTownList(repository);
            Assert.IsNotNull(viewModel.CityList);
            Assert.IsNotNull(viewModel.DistrictList);
            Assert.IsNotNull(viewModel.TownList);
            Assert.IsNull(viewModel.CityName);
            Assert.IsNull(viewModel.DistrictName);
            Assert.IsNull(viewModel.TownName);
            QueryNamesService service = new QueryDistinctCityNamesService(towns);
            Assert.AreEqual(viewModel.CityList.Count, service.QueryCount());
            Assert.AreEqual(viewModel.DistrictList.Count, 0);
            Assert.AreEqual(viewModel.TownList.Count, 0);
            Assert.IsNull(viewModel.ENodebs);
        }

        public void AssertTest(ITownRepository repository, string cityName, string districtName, string townName)
        {
            viewModel.CityName = cityName;
            viewModel.DistrictName = districtName;
            viewModel.TownName = townName;
            viewModel.InitializeTownList(repository, viewModel);
            viewModel.AssertRegionList(towns, viewModel);
        }
    }

    [TestClass]
    public class ENodebQueryViewModelTest : ParametersConfig
    {
        private readonly ENodebQueryViewModel viewModel = new ENodebQueryViewModel();
        private ENodebQueryViewModelTestHelper helper;
        private readonly Mock<ITownRepository> mockTownRepository = new Mock<ITownRepository>();

        [TestInitialize]
        public void TestInitialize()
        {
            mockTownRepository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            helper = new ENodebQueryViewModelTestHelper(towns, viewModel);
        }

        [TestMethod]
        public void TestENodebQueryViewModel()
        {
            helper.AssertTest(mockTownRepository.Object);
        }

        [TestMethod]
        public void TestRegionViewModelInitialize_NotNullTown()
        {
            helper.AssertTest(mockTownRepository.Object, "City1", "District1", "Town1");
        }

        [TestMethod]
        public void TestRegionViewModelInitialize_InexistedTown_1_3_5()
        {
            helper.AssertTest(mockTownRepository.Object, "City1", "District3", "Town5");
        }

        [TestMethod]
        public void TestRegionViewModelInitialize_InexistedTown_2_4_6()
        {
            helper.AssertTest(mockTownRepository.Object, "City2", "District4", "Town6");
        }

        [TestMethod]
        public void TestRegionViewModelInitialize_ExistedTown_1_2_4()
        {
            helper.AssertTest(mockTownRepository.Object, "City1", "District2", "Town4");
        }
    }
}
