using System.Collections.Generic;
using Lte.Parameters.Kpi.Abstract;
using Lte.Parameters.Service.Public;

namespace Lte.Parameters.Abstract
{
    public interface IExcelBtsImportRepository<TBts>
        where TBts : class, IValueImportable, new()
    {
        List<TBts> BtsExcelList { get; }
    }

    public interface IExcelCellImportRepository<TCell>
        where TCell : class, IValueImportable, new()
    {
        List<TCell> CellExcelList { get; }
    }

    public interface IExcelDistributionImportRepository<TCell>
        where TCell : class, IValueImportable, new()
    {
        List<TCell> DistributionExcelList { get; }
    }

    public static class IExcelParametersImportRepositoryOperations
    {
        public static void DumpFromImportedData<TBts>(
            this IExcelBtsImportRepository<TBts> importRepository,
            IBtsDumpRepository<TBts> dumpRepository, IParametersDumpResults results)
            where TBts : class, IValueImportable, new()
        {
            if (importRepository != null)
            {
                dumpRepository.InvokeAction(importRepository);
            }
        }

        public static void DumpFromImportedData<TCell>(
            this IExcelCellImportRepository<TCell> importRepository,
            ICellDumpRepository<TCell> dumpRepository)
            where TCell : class, IValueImportable, new()
        {
            if (importRepository != null)
            {
                dumpRepository.InvokeAction(importRepository);
            }
        }
    }
}
