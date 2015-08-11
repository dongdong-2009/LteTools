using System;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Abstract;
using Lte.Parameters.Kpi.Concrete;
using Lte.Parameters.Kpi.Service;

namespace Lte.Parameters.Service.Public
{
    public interface IParametersDumpResults
    {
        int ENodebs { set; }
        int NewCells { set; }
        int UpdateCells { set; }
        int UpdatePcis { set; }
        int CdmaBts { set; }
        int NewCdmaCells { set; }
        int UpdateCdmaCells { set; }
    }

    public class ParametersDumpGenerator
    {
        private Func<IParametersDumpController, ParametersDumpInfrastructure,
            IBtsDumpRepository<ENodebExcel>> LteENodebDumpGenerator { get; set; }

        private Func<IParametersDumpController, ParametersDumpInfrastructure,
            ICellDumpRepository<CellExcel>> LteCellDumpGenerator { get; set; }

        private Func<IParametersDumpController, ParametersDumpInfrastructure,
            IBtsDumpRepository<BtsExcel>> CdmaBtsDumpGenerator { get; set; }

        private Func<IParametersDumpController, ParametersDumpInfrastructure,
            ICellDumpRepository<CdmaCellExcel>> CdmaCellDumpGenerator { get; set; }

        private Func<IParametersDumpController, ParametersDumpInfrastructure,
            IMmlDumpRepository<CdmaBts, CdmaCell, BtsExcel, CdmaCellExcel>> MmlDumpGenerator { get; set; }

        public ParametersDumpGenerator()
        {
            LteENodebDumpGenerator = (c, i) => new LteENodebDumpRepository(
                c.TownRepository, c.ENodebRepository, i);
            LteCellDumpGenerator = (c, i) => new LteCellDumpRepository(
                c.CellRepository, c.ENodebRepository, c.BtsRepository, c.CdmaCellRepository, i);
            CdmaBtsDumpGenerator = (c, i) => new CdmaBtsDumpRepository(
                c.TownRepository, c.ENodebRepository, c.BtsRepository, i);
            CdmaCellDumpGenerator = (c, i) => new CdmaCellDumpRepository(
                c.BtsRepository, c.CdmaCellRepository, i);
            MmlDumpGenerator = (c, i) => new MmlDumpRepository(c.BtsRepository, c.CdmaCellRepository, i);
        }

        public void DumpLteData(ParametersDumpInfrastructure infrastructure,
            IParametersDumpController controller,
            IParametersDumpConfig config,
            IParametersDumpResults results)
        {
            IBtsDumpRepository<ENodebExcel> btsDumpRepository
                = LteENodebDumpGenerator(controller, infrastructure);
            btsDumpRepository.ImportBts = config.ImportENodeb;
            btsDumpRepository.UpdateBts = config.UpdateENodeb;
            infrastructure.LteENodebRepository.DumpFromImportedData(btsDumpRepository, results);

            ICellDumpRepository<CellExcel> cellDumpRepository
                = LteCellDumpGenerator(controller, infrastructure);
            cellDumpRepository.ImportCell = config.ImportLteCell;
            cellDumpRepository.UpdateCell = config.UpdateLteCell;
            LteCellDumpRepository repository = cellDumpRepository as LteCellDumpRepository;
            if (repository != null)
                repository.UpdatePci = config.UpdatePci;
            infrastructure.LteCellRepository.DumpFromImportedData(cellDumpRepository);
        }

        public void DumpMmlData(ParametersDumpInfrastructure infrastructure,
            IParametersDumpController controller)
        {
            if (infrastructure.MmlListIsEmpty)
            {
                return;
            }
            IMmlDumpRepository<CdmaBts, CdmaCell, BtsExcel, CdmaCellExcel> dumpRepository
                = MmlDumpGenerator(controller, infrastructure);

            foreach (IMmlImportRepository<CdmaBts, CdmaCell, BtsExcel, CdmaCellExcel> repository 
                in infrastructure.MmlRepositoryList)
            {
                dumpRepository.InvokeAction(repository);
            }
        }

        public void DumpCdmaData(ParametersDumpInfrastructure infrastructure,
            IParametersDumpController controller,
            IParametersDumpConfig config,
            IParametersDumpResults results)
        {
            IBtsDumpRepository<BtsExcel> dumpBtsRepository
                = CdmaBtsDumpGenerator(controller, infrastructure);
            dumpBtsRepository.ImportBts = config.ImportBts;
            dumpBtsRepository.UpdateBts = config.UpdateBts;
            infrastructure.CdmaBtsRepository.DumpFromImportedData(dumpBtsRepository, results);

            ICellDumpRepository<CdmaCellExcel> dumpCellRepository
                = CdmaCellDumpGenerator(controller, infrastructure);
            dumpCellRepository.ImportCell = config.ImportCdmaCell;
            dumpCellRepository.UpdateCell = config.UpdateCdmaCell;
            infrastructure.CdmaCellRepository.DumpFromImportedData(dumpCellRepository);

        }
    }
}
