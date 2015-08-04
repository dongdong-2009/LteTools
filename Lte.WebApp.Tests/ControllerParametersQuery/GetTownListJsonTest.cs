﻿using System.Collections;
using System.Collections.Generic;
using Lte.Parameters.Region.Abstract;
using Lte.WebApp.Controllers.Parameters;
using Lte.WebApp.Tests.ControllerParameters;
using Moq;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lte.WebApp.Tests.ControllerParametersQuery
{
    [TestFixture]
    public class GetTownListJsonTest : ParametersConfig
    {
        private readonly Mock<ITownRepository> townRepository = new Mock<ITownRepository>();
        private TownListController controller;

        [SetUp]
        public void TestInitialize()
        {
            townRepository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            controller = new TownListController(townRepository.Object);
        }

        [Test]
        public void TestGetTownList_City1_District1()
        {
            IEnumerable<string> result = controller.GetTownListByCityAndDistrictName("City1", "District1");
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result.ElementAt(0),"Town1");
            Assert.AreEqual(result.ElementAt(1), "Town2");
            Assert.AreEqual(result.ElementAt(2), "Town3");
        }

        [Test]
        public void TestGetTownList_City1_District2()
        {
            IEnumerable<string> result = controller.GetTownListByCityAndDistrictName("City1", "District2");
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.ElementAt(0), "Town1");
            Assert.AreEqual(result.ElementAt(1), "Town4");
        }

        [Test]
        public void TestGetTownList_City2_District1()
        {
            IEnumerable<string> result = controller.GetTownListByCityAndDistrictName("City2", "District1");
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.ElementAt(0), "Town5");
        }
    }
}
