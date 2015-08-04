using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Moq;

namespace Lte.Parameters.Test.Repository.CellRepository
{
    public class CdmaCellRepositoryTestConfig
    {
        protected readonly Mock<ICdmaCellRepository> repository = new Mock<ICdmaCellRepository>();

        protected void Initialize()
        {
            repository.SetupGet(x => x.Cells).Returns(new List<CdmaCell> 
            {
                new CdmaCell
                {
                    BtsId = 1,
                    SectorId = 0,
                    IsOutdoor = false,
                    Frequency1 = 283,
                    Frequency = 64,
                    Height = 32,
                    Azimuth = 55,
                    AntennaGain = 18,
                    MTilt = 3,
                    ETilt = 6,
                    RsPower = 15.2
                }
            }.AsQueryable());
            repository.MockCdmaCellRepositoryDeleteCell();
            repository.MockCdmaCellRepositorySaveCell();
        }
    }
}