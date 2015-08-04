using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Service.Cdma
{
    public abstract class SaveBtsListService
    {
        protected readonly IBtsRepository _repository;

        protected SaveBtsListService(IBtsRepository repository)
        {
            _repository = repository;
        }

        public abstract void Save(ParametersDumpInfrastructure infrastructure, bool updateBts);
    }

    public class ByDbInfoSaveBtsListService : SaveBtsListService
    {
        private readonly IEnumerable<CdmaBts> _btsInfoList;

        public ByDbInfoSaveBtsListService(IBtsRepository repository,
            IEnumerable<CdmaBts> btsInfoList)
            : base(repository)
        {
            _btsInfoList = btsInfoList;
        }

        public override void Save(ParametersDumpInfrastructure infrastructure, bool updateBts)
        {
            using (var baseRepository = new ENodebBaseRepository(_repository))
            {
                foreach (var cdmaBts in _btsInfoList)
                {
                    var bts = baseRepository.QueryENodeb(cdmaBts.BtsId);
                    if (bts != null) continue;
                    _repository.AddOneBts(cdmaBts);
                }
                _repository.SaveChanges();
            }
            infrastructure.CdmaBtsUpdated = 0;
        }
    }

    public class ByExcelInfoSaveBtsListService : SaveBtsListService
    {
        private readonly IEnumerable<BtsExcel> _btsInfoList;
        private readonly ITownRepository _townRepository;
        private readonly IENodebRepository _lteRepository;
        private readonly ENodebBaseRepository _baseRepository;

        public ByExcelInfoSaveBtsListService(IBtsRepository repository,
            IEnumerable<BtsExcel> btsInfoList, ITownRepository townRepository,
            IENodebRepository lteRepository = null)
            : base(repository)
        {
            _btsInfoList = btsInfoList;
            _townRepository = townRepository;
            _lteRepository = lteRepository;
            _baseRepository = new ENodebBaseRepository(repository);
        }

        public override void Save(ParametersDumpInfrastructure infrastructure, bool updateBts)
        {
            infrastructure.CdmaBtsUpdated = 0;
            IEnumerable<Town> townList = _townRepository.Towns.ToList();
            List<ENodeb> eNodebList = (_lteRepository == null) ? null : _lteRepository.GetAllList();
            TownIdAssignedSaveOneBtsService service = new TownIdAssignedSaveOneBtsService(
                _repository, _baseRepository, null, 0, eNodebList);

            foreach (BtsExcel btsExcel in _btsInfoList.Distinct(new BtsExcelComparer()))
            {
                var town = townList.FirstOrDefault(x => x.DistrictName == btsExcel.DistrictName
                                                        && x.TownName == btsExcel.TownName);
                var townId = (town == null) ? -1 : town.Id;
                service.BtsExcel = btsExcel;
                service.TownId = townId;
                if (service.SaveOneBts(updateBts))
                {
                    infrastructure.CdmaBtsUpdated++;
                }
            }
            _repository.SaveChanges();
        }
    }
}
