using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Service.Cdma
{
    public class QueryCdmaCellService
    {
        private readonly ICdmaCellRepository _repository;
        private readonly int _btsId;
        private readonly byte _sectorId;
        private readonly string _cellType;

        public QueryCdmaCellService(ICdmaCellRepository repository,
            int btsId, byte sectorId, string cellType)
        {
            _repository = repository;
            _btsId = btsId;
            _sectorId = sectorId;
            _cellType = cellType;
        }

        public CdmaCell Query()
        {
            return _repository.Cells.FirstOrDefault(
                x => x.BtsId == _btsId && x.SectorId == _sectorId && x.CellType == _cellType);
        }
    }
}
