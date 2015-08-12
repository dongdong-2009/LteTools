using System.Collections.Generic;

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
}
