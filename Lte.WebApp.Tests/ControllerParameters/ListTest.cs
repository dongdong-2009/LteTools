using Lte.Evaluations.Service;
using Lte.Evaluations.ViewHelpers;
using Lte.Parameters.Region.Abstract;
using Lte.WebApp.Controllers.Parameters;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Lte.Parameters.Abstract;
using System.Web.Mvc;
using Lte.Parameters.Region;
using NUnit.Framework;

namespace Lte.WebApp.Tests.ControllerParameters
{
    [TestFixture]
    public class ListTest : ParametersConfig
    {
        private ParametersController controller;

        [SetUp]
        public void TestInitialize()
        {
            Mock<ITownRepository> townRepository = new Mock<ITownRepository>();
            Mock<IENodebRepository> eNodebRepository = new Mock<IENodebRepository>();
            Mock<IRegionRepository> regionRepository = new Mock<IRegionRepository>();
            townRepository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            eNodebRepository.SetupGet(x => x.GetAll()).Returns(eNodebs.AsQueryable());
            controller = new ParametersController(townRepository.Object, eNodebRepository.Object, null, null, null, 
                regionRepository.Object, null);
        }

        [Test]
        public void TestList()
        {
            ParametersContainer container = new ParametersContainer();
            ViewResult viewResult = controller.List(container);
            IEnumerable<TownENodebStat> stats = viewResult.Model as IEnumerable<TownENodebStat>;
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), 7);
            Assert.AreEqual(container.TownENodebStats.Count(), 7);
            Assert.AreEqual(container.TownENodebStats.ElementAt(0).TotalENodebs, 2);
            Assert.AreEqual(container.TownENodebStats.ElementAt(1).TotalENodebs, 1);
            Assert.AreEqual(container.TownENodebStats.ElementAt(2).TotalENodebs, 1);
            Assert.AreEqual(container.TownENodebStats.ElementAt(3).TotalENodebs, 0);
            Assert.AreEqual(container.TownENodebStats.ElementAt(4).TotalENodebs, 2);
            Assert.AreEqual(container.TownENodebStats.ElementAt(5).TotalENodebs, 1);
            Assert.AreEqual(container.TownENodebStats.ElementAt(6).TotalENodebs, 2);
        }
    }
}
