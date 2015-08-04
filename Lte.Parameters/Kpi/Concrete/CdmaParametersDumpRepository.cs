using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Abstract;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Service.Cdma;

namespace Lte.Parameters.Kpi.Concrete
{
    public class CdmaBtsDumpRepository : IBtsDumpRepository<BtsExcel>
    {
        private readonly ITownRepository townRepository;
        private readonly IENodebRepository eNodebRepository;
        private readonly IBtsRepository btsRepository;
        private readonly ParametersDumpInfrastructure infrastructure;

        public bool ImportBts { get; set; }
        public bool UpdateBts { get; set; }

        public CdmaBtsDumpRepository(
            ITownRepository townRepository,
            IENodebRepository eNodebRepository, 
            IBtsRepository btsRepository,
            ParametersDumpInfrastructure infrastructure)
        {
            this.townRepository = townRepository;
            this.eNodebRepository = eNodebRepository;
            this.btsRepository = btsRepository;
            this.infrastructure = infrastructure;
        }

        public void InvokeAction(IExcelBtsImportRepository<BtsExcel> importRepository)
        {
            if (!ImportBts || importRepository.BtsExcelList.Count <= 0) return;
            SaveBtsListService service = new ByExcelInfoSaveBtsListService(
                btsRepository, importRepository.BtsExcelList, townRepository, eNodebRepository);
            service.Save(infrastructure, UpdateBts);
        }
    }

    public class CdmaCellDumpRepository : ICellDumpRepository<CdmaCellExcel>
    {
        public bool ImportCell { get; set; }

        public bool UpdateCell { get; set; }

        private readonly ParametersDumpInfrastructure infrastructure;
        private readonly IBtsRepository btsRepository;
        private readonly ICdmaCellRepository cdmaCellRepository;

        public CdmaCellDumpRepository(IBtsRepository btsRepository,
            ICdmaCellRepository cdmaCellRepository,
            ParametersDumpInfrastructure infrastructure)
        {
            this.btsRepository = btsRepository;
            this.cdmaCellRepository = cdmaCellRepository;
            this.infrastructure = infrastructure;
        }

        public void InvokeAction(IExcelCellImportRepository<CdmaCellExcel> importRepository)
        {
            if (ImportCell && importRepository.CellExcelList.Count > 0)
            {
                SaveCdmaCellInfoListService service =
                    new UpdateConsideredSaveCdmaCellInfoListService(cdmaCellRepository,
                        importRepository.CellExcelList, btsRepository, UpdateCell);
                service.Save(infrastructure);
            }
        }
    }
}
