using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Region.Service;
using Lte.Parameters.Service.Public;

namespace Lte.Parameters.Service.Lte
{
    public class SaveENodebListService
    {
        private readonly IENodebRepository _repository;
        private readonly ITownRepository _townRepository;
        private readonly ENodebBaseRepository _baseRepository;
        private readonly List<ENodebExcel> _eNodebInfoList;

        private static Func<ENodebExcel, bool> infoFilter = x => x.ENodebId > 10000;

        public SaveENodebListService(IENodebRepository repository,
            List<ENodebExcel> eNodebInfoList, ITownRepository townRepository)
        {
            _repository = repository;
            _baseRepository = new ENodebBaseRepository(repository);
            _townRepository = townRepository;
            _eNodebInfoList = eNodebInfoList;
        }

        public static Func<ENodebExcel, bool> InfoFilter
        {
            set { infoFilter = value; }
        }

        public void Save(ParametersDumpInfrastructure infrastructure, IParametersDumpResults results, bool update)
        {
            infrastructure.ENodebsUpdated = 0;
            SaveOneENodebService saveService = new TownAssignedSaveOneENodebService(
                _repository, _baseRepository, update);
            IEnumerable<ENodebExcel> validInfos =
                _eNodebInfoList.Where(x => infoFilter(x)).Distinct(new ENodebExcelComparer()).Distinct(
                new ENodebExcelNameComparer());

            foreach (ENodebExcel info in validInfos)
            {
                int townId = _townRepository.Towns.ToList().QueryId(info);
                if (saveService.Save(info, townId))
                {
                    infrastructure.ENodebsUpdated++;
                    results.ENodebs = infrastructure.ENodebsUpdated;
                }
            }
        }
    }

    public abstract class SaveOneENodebService
    {
        protected readonly IENodebRepository _repository;

        protected SaveOneENodebService(IENodebRepository repository)
        {
            _repository = repository;
        }

        public abstract bool Save(ENodebExcel eNodebInfo, int townId);
    }

    public class TownMatchedSaveOneENodebService : SaveOneENodebService
    {
        private readonly IEnumerable<Town> _towns;

        public TownMatchedSaveOneENodebService(IENodebRepository repository, ITownRepository townRepository)
            : base(repository)
        {
            _towns = townRepository.Towns.ToList();
        }

        public override bool Save(ENodebExcel eNodebInfo, int townId)
        {
            townId = _towns.QueryId(eNodebInfo);
            ENodeb existedENodebWithSameName =
                _repository.GetAll().FirstOrDefault(x => x.TownId == townId && x.Name == eNodebInfo.Name);
            ENodeb existedENodebWithSameId = _repository.GetAll().FirstOrDefault(x => x.ENodebId == eNodebInfo.ENodebId);

            if (existedENodebWithSameName == null && existedENodebWithSameId == null)
            {
                existedENodebWithSameId = new ENodeb();
                existedENodebWithSameId.Import(eNodebInfo, townId, false);
                _repository.Insert(existedENodebWithSameId);
            }
            else if (existedENodebWithSameName == existedENodebWithSameId)
            {
                existedENodebWithSameId.Import(eNodebInfo, townId, false);
            }
            else
            {
                return false;
            }

            return true;
        }
    }

    public class TownAssignedSaveOneENodebService : SaveOneENodebService
    {
        private readonly bool _updateExisted;
        private readonly ENodebBaseRepository _baseRepository;

        public TownAssignedSaveOneENodebService(IENodebRepository repository,
            ENodebBaseRepository baseRepository, bool updateExisted = false)
            : base(repository)
        {
            _baseRepository = baseRepository;
            _updateExisted = updateExisted;
        }

        public override bool Save(ENodebExcel eNodebInfo, int townId)
        {
            ENodebBase existedENodebWithSameName = _baseRepository.QueryENodeb(townId, eNodebInfo.Name);
            ENodebBase existedENodebWithSameId = _baseRepository.QueryENodeb(eNodebInfo.ENodebId);
            if (existedENodebWithSameName == null && existedENodebWithSameId == null)
            {
                ENodeb eNodeb = new ENodeb();
                eNodeb.Import(eNodebInfo, townId);
                _repository.Insert(eNodeb);
                return true;
            }
            if (!_updateExisted || existedENodebWithSameName != existedENodebWithSameId) return false;
            ENodeb byIdENodeb = _repository.GetAll().FirstOrDefault(x => x.ENodebId == eNodebInfo.ENodebId);
            if (byIdENodeb != null) byIdENodeb.Import(eNodebInfo, townId, false);
            return true;
        }
    }
}
