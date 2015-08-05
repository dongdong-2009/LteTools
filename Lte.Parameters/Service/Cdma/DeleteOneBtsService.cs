using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Service;

namespace Lte.Parameters.Service.Cdma
{
    public class DeleteOneBtsService
    {
        private readonly IBtsRepository _repository;
        private readonly CdmaBts _bts;

        public DeleteOneBtsService(IBtsRepository repository)
        {
            _repository = repository;
        }

        public DeleteOneBtsService(IBtsRepository repository, CdmaBts bts)
            : this(repository)
        {
            _bts = bts;
        }

        public DeleteOneBtsService(IBtsRepository repository, int townId, string btsName)
            : this(repository)
        {
            QueryBtsService byNameBtsService
                = new ByTownIdAndNameQueryBtsService(repository, townId, btsName);
            _bts = byNameBtsService.QueryBts();
        }

        public DeleteOneBtsService(IBtsRepository repository, int btsId)
            : this(repository)
        {
            _bts = repository.GetAll().FirstOrDefault(x=>x.BtsId==btsId);
        }

        public DeleteOneBtsService(IBtsRepository repository, ITownRepository townRepository,
            string districtName, string townName, string btsName)
            : this(repository)
        {
            int townId = townRepository.Towns.ToList().QueryId(districtName, townName);
            QueryBtsService byNameBtsService
                = new ByTownIdAndNameQueryBtsService(repository, townId, btsName);
            _bts = byNameBtsService.QueryBts();
        }

        public bool Delete()
        {
            if (_bts == null) return false;
            _repository.Delete(_bts);
            return true;
        }
    }
}
