using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Service.Cdma
{
    public class DeleteOneCdmaCellService
    {
        private readonly ICdmaCellRepository _repository;
        private readonly CdmaCell _cell;

        public DeleteOneCdmaCellService(ICdmaCellRepository repository,
            int btsId, byte sectorId, string cellType)
        {
            _repository = repository;
            QueryCdmaCellService cellService
                = new QueryCdmaCellService(repository, btsId, sectorId, cellType);
            _cell = cellService.Query();
        }

        public bool Delete()
        {
            bool result = (_cell != null) && _repository.RemoveOneCell(_cell);
            if (!result) return false;
            _repository.SaveChanges();
            return true;
        }
    }
}
