using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Service;

namespace Lte.Parameters.Service.Cdma
{
    public abstract class SaveOneBtsService
    {
        protected readonly IBtsRepository _repository;

        protected BtsExcel _btsInfo;

        protected int _townId;

        public abstract bool SaveOneBts(bool updateBts);

        protected SaveOneBtsService(IBtsRepository repository,
            BtsExcel btsInfo)
        {
            _repository = repository;
            _btsInfo = btsInfo;
        }
    }

    public class TownListConsideredSaveOneBtsService : SaveOneBtsService
    {
        private readonly QueryBtsService _byNameBtsService;

        public TownListConsideredSaveOneBtsService(IBtsRepository repository,
            BtsExcel btsInfo, ITownRepository townRepository)
            : base(repository, btsInfo)
        {
            _townId = townRepository.Towns.QueryId(btsInfo.DistrictName, btsInfo.TownName);
            _byNameBtsService
                = new ByTownIdAndNameQueryBtsService(repository, _townId, btsInfo.Name);
        }

        public override bool SaveOneBts(bool updateBts)
        {
            CdmaBts bts = _byNameBtsService.QueryBts();
            CdmaBts existedENodebWithSameId = _repository.Btss.FirstOrDefault(x => x.BtsId == _btsInfo.BtsId);
            bool addENodeb = false;

            if (bts == null)
            {
                bts = existedENodebWithSameId;
                if (bts == null)
                {
                    bts = new CdmaBts();
                    addENodeb = true;
                }
            }
            else if (bts != existedENodebWithSameId) { return false; }

            if (addENodeb)
            {
                bts.TownId = _townId;
                bts.Import(_btsInfo, true);
                _repository.AddOneBts(bts);
            }
            else if (updateBts)
            {
                const double tolerance = 1E-6;
                if (Math.Abs(bts.Longtitute) < tolerance && Math.Abs(bts.Lattitute) < tolerance)
                {
                    bts.TownId = _townId;
                    bts.Import(_btsInfo, false);
                }
                else
                { return false; }
            }
            _repository.SaveChanges();
            return true;
        }
    }

    public class TownIdAssignedSaveOneBtsService : SaveOneBtsService
    {
        private readonly ENodebBaseRepository _baseRepository;
        private readonly List<ENodeb> _eNodebList;

        public TownIdAssignedSaveOneBtsService(IBtsRepository repository,
            ENodebBaseRepository baseRepository,
            BtsExcel btsInfo, int townId,
            List<ENodeb> eNodebList = null)
            : base(repository, btsInfo)
        {
            _baseRepository = baseRepository;
            _eNodebList = eNodebList;
            _townId = townId;
        }

        public int TownId
        {
            set { _townId = value; }
        }

        public BtsExcel BtsExcel 
        {
            set { _btsInfo = value; }
        }

        public override bool SaveOneBts(bool updateBts)
        {
            ENodebBase eNodebBase = _baseRepository.QueryENodeb(_btsInfo.BtsId);

            CdmaBts bts;
            if (eNodebBase == null)
            {
                bts = new CdmaBts();
                bts.TownId = _townId;
                bts.Import(_btsInfo, true);
                bts.ImportLteInfo(_eNodebList);
                _repository.AddOneBts(bts);
                return true;
            }
            if (!updateBts) return false;
            bts = _repository.Btss.FirstOrDefault(x => x.BtsId == _btsInfo.BtsId);
            if (bts != null)
            {
                bts.TownId = _townId;
                bts.Import(_btsInfo, false);
                bts.ImportLteInfo(_eNodebList);
            }
            return true;
        }
    }
}
