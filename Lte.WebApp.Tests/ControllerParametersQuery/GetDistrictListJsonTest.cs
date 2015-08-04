using System.Collections;
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
    public class GetDistrictListJsonTest : ParametersConfig
    {
        private readonly Mock<ITownRepository> townRepository = new Mock<ITownRepository>();
        private DistrictListController controller;

        [SetUp]
        public void TestInitialize()
        {
            townRepository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            controller = new DistrictListController(townRepository.Object);
        }

        [Test]
        public void TestGetDistrictList_City1()
        {
            IEnumerable<string> result = controller.GetDistrictListByCityName("City1");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.ElementAt(0), "District1");
            Assert.AreEqual(result.ElementAt(1), "District2");
        }

        [Test]
        public void TestGetDistrictList_City2()
        {
            IEnumerable<string> result = controller.GetDistrictListByCityName("City2");
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.ElementAt(0), "District1");
            Assert.AreEqual(result.ElementAt(1), "District3");
        }
    }
}
