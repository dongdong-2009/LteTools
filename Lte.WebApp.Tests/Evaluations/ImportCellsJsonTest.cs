using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lte.Evaluations.Infrastructure;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.WebApp.Controllers.Topic;
using NUnit.Framework;
using Moq;

namespace Lte.WebApp.Tests.Evaluations
{
    [TestFixture]
    public class ImportCellsJsonTest
    {
        private readonly Mock<ICellRepository> cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> eNodebRepository = new Mock<IENodebRepository>();
        private EvaluationController controller;
        private readonly EvaluationInfrastructure infrastructure 
            = new EvaluationInfrastructure();

        [SetUp]
        public void TestInitialize()
        {
            eNodebRepository.SetupGet(x => x.GetAll()).Returns(new List<ENodeb> 
            {
                new ENodeb { ENodebId = 1, Name = "E-1" },
                new ENodeb { ENodebId = 2, Name = "E-2" },
                new ENodeb { ENodebId = 3, Name = "E-3" }
            }.AsQueryable());
            cellRepository.SetupGet(x => x.Cells).Returns(new List<Cell>
            {
                new Cell { ENodebId = 1, SectorId = 1, Height = 10 },
                new Cell { ENodebId = 1, SectorId = 2, Height = 10 },
                new Cell { ENodebId = 1, SectorId = 3, Height = 10 },
                new Cell { ENodebId = 2, SectorId = 1, Height = 10 },
                new Cell { ENodebId = 2, SectorId = 2, Height = 10 },
                new Cell { ENodebId = 2, SectorId = 3, Height = 10 },
                new Cell { ENodebId = 3, SectorId = 1, Height = 10 },
                new Cell { ENodebId = 3, SectorId = 2, Height = 10 },
                new Cell { ENodebId = 3, SectorId = 3, Height = 10 }
            }.AsQueryable());
            controller = new EvaluationController(null, eNodebRepository.Object, cellRepository.Object);
            controller.ResetENodebList();
        }

        [TestCase("1,2,3,", 9)]
        [TestCase("4,5,6,", 0)]
        [TestCase("2,5,6,", 3)]
        public void TestImportCellsJson_FullIdList(string message, int length)
        {
            JsonResult results = controller.ImportCells(infrastructure, message);
            Assert.IsNotNull(results.Data);
            object[] cellInfo = results.Data as object[];
            Assert.IsNotNull(cellInfo);
            Assert.AreEqual(cellInfo.Length, length);
        }

    }
}
