using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lte.Domain.TypeDefs;
using Lte.Evaluations.Kpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Kpi.Abstract;
using Lte.Parameters.Kpi.Entities;
using Lte.Parameters.Kpi.Service;

namespace Lte.WebApp.Controllers.Kpi
{
    public class TopDrop2GController : Controller
    {
        private readonly ITopCellRepository<TopDrop2GCell> _statRepository;
        private readonly ITopCellRepository<TopDrop2GCellDaily> _dailyStatRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopDrop2GController(
            ITopCellRepository<TopDrop2GCell> statRepository,
            ITopCellRepository<TopDrop2GCellDaily> dailyStatRepository,
            IBtsRepository btsRepository,
            IENodebRepository eNodebRepository)
        {
            _statRepository = statRepository;
            _dailyStatRepository = dailyStatRepository;
            _btsRepository = btsRepository;
            _eNodebRepository = eNodebRepository;
        }

        public JsonResult Query(DateTime begin, DateTime end, string city, int topCounts = 20)
        {
            DateTime endDate = end.AddDays(1);
            IEnumerable<TopDrop2GCell> stats = _statRepository.Stats.QueryTimeStats(begin, endDate, city).ToList();
            if (stats.Any())
            {
                return Json(
                    stats.QueryTopCounts<TopDrop2GCell, TopDrop2GCellView>(_btsRepository, _eNodebRepository, topCounts), 
                    JsonRequestBehavior.AllowGet);
            }
            return Json(new List<int>(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryDaily(DateTime begin, DateTime end, int topCounts = 20)
        {
            DateTime endDate = end.AddDays(1);
            IEnumerable<TopDrop2GCellDaily> stats = _dailyStatRepository.Stats.QueryTimeStats(begin, endDate).ToList();
            if (stats.Any())
            {
                return Json(
                    stats.QueryTopCounts<TopDrop2GCellDaily, TopDrop2GCellDailyView>(_btsRepository, _eNodebRepository, topCounts), 
                    JsonRequestBehavior.AllowGet);
            }
            return Json(new List<int>(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryHistory(int cellId, byte sectorId, short frequency, DateTime end, int days = 20)
        { 
            DateTime beginDate = end.AddDays(-days).Date;
            DateTime endDate = end.AddDays(1).Date;
            IEnumerable<TopDrop2GCell> cells = new List<TopDrop2GCell>();
            while (cells.Count() < 3 && beginDate > endDate.AddDays(-300))
            {
                cells = _statRepository.Stats.Where(
                    x => x.StatTime >= beginDate && x.StatTime < endDate
                    && x.CellId == cellId && x.SectorId == sectorId && x.Frequency == frequency).ToList();
                beginDate = beginDate.AddDays(-days);
            }
            return Json(cells.OrderBy(x => x.StatTime).Select(x => new
            {
                StatDate = x.StatTime.Date.ToShortDateString(), x.Drops,
                MoCalls = x.MoAssignmentSuccess,
                MtCalls = x.MtAssignmentSuccess, x.DropRate
            }),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryDistanceDistribution(int cellId, byte sectorId, short frequency, DateTime end)
        {
            QueryTopDrop2GService service = new QueryTopDrop2GService(_dailyStatRepository,
                cellId, sectorId, frequency, end);

            List<DistanceDistribution> result = service.GenerateDistanceDistribution();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryCoverageInterferenceDistribution(
            int cellId, byte sectorId, short frequency, DateTime end)
        {
            QueryTopDrop2GService service = new QueryTopDrop2GService(_dailyStatRepository,
                cellId, sectorId, frequency, end);

            List<CoverageInterferenceDistribution> result = service.GenerateCoverageInterferenceDistribution();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryDropsHourDistribution(int cellId, byte sectorId, short frequency, DateTime end)
        {
            QueryTopDrop2GService service = new QueryTopDrop2GService(_dailyStatRepository,
                cellId, sectorId, frequency, end);

            List<DropsHourDistribution> result = service.GenerateDropsHourDistribution();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryAlarmHourDistribution(int cellId, byte sectorId, short frequency, DateTime end)
        {
            QueryTopDrop2GService service = new QueryTopDrop2GService(_dailyStatRepository,
               cellId, sectorId, frequency, end);

            AlarmHourDistribution distribution = service.GenerateAlarmHourDistribution();
            return Json(distribution.AlarmRecords.Select(x => new
            {
                Type = x.Key,
                Data = x.Value
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryHistoryDaily(int cellId, byte sectorId, short frequency, DateTime end, int days = 20)
        {
            DateTime beginDate = end.AddDays(-days).Date;
            DateTime endDate = end.AddDays(1).Date;
            IEnumerable<TopDrop2GCellDaily> cells = new List<TopDrop2GCellDaily>();
            while (cells.Count() < 3 && beginDate > endDate.AddDays(-300))
            {
                cells = _dailyStatRepository.Stats.Where(
                    x => x.StatTime >= beginDate && x.StatTime < endDate
                    && x.CellId == cellId && x.SectorId == sectorId && x.Frequency == frequency).ToList();
                beginDate = beginDate.AddDays(-days);
            }
            return Json(cells.OrderBy(x => x.StatTime).Select(x => new
            {
                StatDate = x.StatTime.Date.ToShortDateString(),
                Drops = x.CdrDrops,
                DropRate = x.CdrDropRate,
                DropDistance = x.AverageDropDistance,
                DropRssi = x.AverageRssi
            }),
                JsonRequestBehavior.AllowGet);
        }
	}
}