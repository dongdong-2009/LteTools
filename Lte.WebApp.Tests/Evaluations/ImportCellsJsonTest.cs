using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lte.Evaluations.Infrastructure;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.WebApp.Controllers.Topic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lte.WebApp.Tests.Evaluations
{
    [TestClass]
    public class ImportCellsJsonTest
    {
        private readonly Mock<ICellRepository> cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> eNodebRepository = new Mock<IENodebRepository>();
        private EvaluationController controller;
        private readonly EvaluationInfrastructure infrastructure 
            = new EvaluationInfrastructure();

        [TestInitialize]
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

        [TestMethod]
        public void TestImportCellsJson_FullIdList()
        {
            
            string message = "1,2,3,";
            JsonResult results = controller.ImportCells(infrastructure, message);
            Assert.IsNotNull(results.Data);
            object[] cellInfo = results.Data as object[];
            Assert.IsNotNull(cellInfo);
            Assert.AreEqual(cellInfo.Length, 9);
        }

        [TestMethod]
        public void TestImportCellsJson_NoneIdMatched()
        {
            
            string message = "4,5,6,";
            JsonResult results = controller.ImportCells(infrastructure, message);
            Assert.IsNotNull(results);
            object[] cellInfo = results.Data as object[];
            Assert.IsNotNull(cellInfo);
            Assert.AreEqual(cellInfo.Length, 0);
        }

        [TestMethod]
        public void TestImportCellsJson_OneIdMatched()
        {
            infrastructure.CellList.Clear();
            string message = "2,5,6,";
            JsonResult results = controller.ImportCells(infrastructure, message);
            Assert.IsNotNull(results);
            object[] cellInfo = results.Data as object[];
            Assert.IsNotNull(cellInfo);
            Assert.AreEqual(cellInfo.Length, 3);
        }
    }
}
