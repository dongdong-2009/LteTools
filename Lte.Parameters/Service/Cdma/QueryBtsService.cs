using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Service.Cdma
{
    public abstract class QueryBtsService
    {
        protected IBtsRepository _repository;

        protected QueryBtsService(IBtsRepository repository)
        {
            _repository = repository;
        }

        public abstract CdmaBts QueryBts();
    }

    public class ByTownIdAndNameQueryBtsService : QueryBtsService
    {
        private readonly int _townId;
        private readonly string _name;

        public ByTownIdAndNameQueryBtsService(IBtsRepository repository, int townId, string name)
            : base(repository)
        {
            _townId = townId;
            _name = name;
        }

        public override CdmaBts QueryBts()
        {
            return _repository.GetAll().FirstOrDefault(x => x.TownId == _townId && x.Name == _name);
        }
    }
}
