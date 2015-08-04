﻿using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Geo.Abstract;
using Lte.Evaluations.ViewHelpers;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Region.Service;
using Lte.WebApp.Tests.ControllerParameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lte.WebApp.Tests.Parameters
{
    internal class RegionViewModelInitializeTestHelper
    {
        private readonly IEnumerable<Town> towns;
        private readonly RegionViewModel viewModel;
        private readonly QueryNamesService service;

        public RegionViewModelInitializeTestHelper(IEnumerable<Town> towns, RegionViewModel viewModel)
        {
            this.towns = towns;
            this.viewModel = viewModel;
            service = new QueryDistinctCityNamesService(towns);
        }

        public void AssertTest(ITownRepository repository, ITown town)
        {
            viewModel.InitializeTownList(repository, town);
            Assert.IsNotNull(viewModel.CityList);
            Assert.IsNotNull(viewModel.DistrictList);
            Assert.IsNotNull(viewModel.TownList);

            if (town == null)
            {
                Assert.IsNull(viewModel.CityName);
                Assert.IsNull(viewModel.DistrictName);
                Assert.IsNull(viewModel.TownName);
                Assert.AreEqual(viewModel.CityList.Count, service.QueryCount());
                Assert.AreEqual(viewModel.DistrictList.Count, 0);
                Assert.AreEqual(viewModel.TownList.Count, 0);
            }
            else
            {
                Assert.IsNotNull(viewModel.CityList);
                Assert.IsNotNull(viewModel.DistrictList);
                Assert.IsNotNull(viewModel.TownList);
                Assert.AreEqual(viewModel.CityName, town.CityName);
                Assert.AreEqual(viewModel.DistrictName, town.DistrictName);
                Assert.AreEqual(viewModel.TownName, town.TownName);
                viewModel.AssertRegionList(towns, town);
            }
        }

        public void AssertTest(ITownRepository repository, string cityName, string districtName, string townName)
        {
            Mock<ITown> mockTown = new Mock<ITown>();
            mockTown.SetupGet(x => x.CityName).Returns(cityName);
            mockTown.SetupGet(x => x.DistrictName).Returns(districtName);
            mockTown.SetupGet(x => x.TownName).Returns(townName);
            AssertTest(repository, mockTown.Object);
        }
    }

    [TestClass]
    public class RegionViewModelInitializeTest : ParametersConfig
    {
        private readonly RegionViewModel viewModel = new RegionViewModel("");
        private RegionViewModelInitializeTestHelper helper; 
        private readonly Mock<ITownRepository> mockTownRepository = new Mock<ITownRepository>();

        [TestInitialize]
        public void TestInitialize()
        {
            mockTownRepository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            helper = new RegionViewModelInitializeTestHelper(towns, viewModel);
        }

        [TestMethod]
        public void TestRegionViewModelInitialize_NullTown()
        {
            helper.AssertTest(mockTownRepository.Object, null);
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
