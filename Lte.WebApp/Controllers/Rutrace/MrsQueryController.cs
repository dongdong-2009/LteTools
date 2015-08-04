using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.Service;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.WebApp.Controllers.Rutrace
{
    public class MrsQueryCoverageController : ApiController
    {
        [Route("api/MrsQueryCoverage/{topNum}")]
        public IEnumerable<MrsCellDateView> Get(int topNum)
        {
            return RutraceStatContainer.MrsStats.Take(topNum);
        }
    }

    public class MrsQueryTaController : ApiController
    {
        private readonly IMrsCellRepository _repository;

        public MrsQueryTaController(IMrsCellRepository repository)
        {
            _repository = repository;
        }

        [Route("api/MrsQueryTa/{cellId}/{sectorId}/{date}")]
        public MrsCellTa Get(int cellId, byte sectorId, DateTime date)
        {
            return _repository.TaCells.FirstOrDefault(x =>
                x.CellId == cellId && x.SectorId == sectorId && x.RecordDate == date);
        }
    }

    public class MroQueryRsrpTaController : ApiController
    {
        private readonly IMroCellRepository _repository;

        public MroQueryRsrpTaController(IMroCellRepository repository)
        {
            _repository = repository;
        }

        [Route("api/MroQueryRsrpTa/{cellId}/{sectorId}/{date}")]
        public IEnumerable<MroRsrpTa> Get(int cellId, byte sectorId, DateTime date)
        {
            return _repository.RsrpTaCells.Where(x =>
                x.CellId == cellId && x.SectorId == sectorId && x.RecordDate == date);
        }
    }
}
