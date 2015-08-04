using System.Linq;
using Lte.Evaluations.Service;
using Lte.Evaluations.ViewHelpers;
using Lte.Parameters.Region.Abstract;
using Lte.WebApp.Controllers.Parameters;
using Moq;
using Lte.Parameters.Abstract;
using System.Web.Mvc;
using Lte.Parameters.Region;
using NUnit.Framework;

namespace Lte.WebApp.Tests.ControllerParameters
{
    internal class ENodebListTestHelper
    {
        private readonly ParametersController controller;

        public ENodebListTestHelper(ParametersController controller)
        {
            this.controller = controller;
        }

        public void AssertTest(int townId, int expectedENodebs)
        {
            ViewResult viewResult = controller.ENodebList(townId);
            ENodebListViewModel viewModel = viewResult.Model as ENodebListViewModel;
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel.TownId, townId);
            Assert.AreEqual(viewModel.Items.Count(), expectedENodebs);
            Assert.AreEqual(viewModel.QueryItems.Count(), expectedENodebs);
        }
    }

    [TestFixture]
    public class ENodebListTest : ParametersConfig
    {
        private readonly Mock<IENodebRepository> eNodebRepository = new Mock<IENodebRepository>();
        private readonly ParametersContainer container = new ParametersContainer();
        private ParametersController controller;
        private ENodebListTestHelper helper;
        private readonly Mock<ITownRepository> mockTownRepository = new Mock<ITownRepository>();
        private readonly Mock<IRegionRepository> mockRegionRepository =
            new Mock<IRegionRepository>();

        [SetUp]
        public void TestInitialize()
        {
            mockTownRepository.SetupGet(x => x.Towns).Returns(towns.AsQueryable());
            eNodebRepository.SetupGet(x => x.GetAll()).Returns(lotsOfENodebs.AsQueryable());
            controller = new ParametersController(mockTownRepository.Object, eNodebRepository.Object, null, null, null,
                mockRegionRepository.Object, null);
            helper = new ENodebListTestHelper(controller);
        }

        [Test]
        public void TestENodebList_TownId1_Expected4()
        {
            helper.AssertTest(1, 4);
        }

        [Test]
        public void TestENodebList_TownId2_Expected2()
        {
            helper.AssertTest(2, 2);
        }

        [Test]
        public void TestENodebList_TownId5_Expected6()
        {
            helper.AssertTest(5, 6);
        }

        [Test]
        public void TestENodebList_TownId7_Expected3()
        {
            helper.AssertTest(7, 3);
        }
    }
}
