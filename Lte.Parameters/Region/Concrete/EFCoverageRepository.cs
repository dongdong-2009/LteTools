using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Service.Coverage;

namespace Lte.Parameters.Region.Concrete
{
    public class EFCoverageRepository : ICoverageRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();
        private readonly CoverageAdjustmentService _service;

        public EFCoverageRepository()
        {
            _service = new CoverageAdjustmentService(this);
        }

        public IQueryable<CoverageAdjustment> CoverageAdjustments
        {
            get 
            {
                return context.CoverageAdjustments;
            }
        }

        public void Save(IEnumerable<CoverageAdjustment> adjustments)
        {
            foreach (CoverageAdjustment adj in adjustments)
            {
                CoverageAdjustment item = _service.QueryItem(adj);
                if (item == null)
                {
                    context.CoverageAdjustments.Add(adj);
                    context.SaveChanges();
                }
                else
                {
                    adj.CloneProperties<CoverageAdjustment>(item);
                }
            }
            context.SaveChanges();
        }
    }

    public class EFInterferenceStatRepository : IInterferenceStatRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<InterferenceStat> InterferenceStats
        {
            get { return context.InterferenceStats; }
        }

        public IQueryable<PureInterferenceStat> PureInterferenceStats
        {
            get { return context.PureInterferenceStats; }
        }

        public void Save(IEnumerable<InterferenceStat> stats)
        {
            foreach (InterferenceStat stat in stats)
            {
                InterferenceStat item = InterferenceStats.FirstOrDefault(x =>
                    x.CellId == stat.CellId && x.SectorId == stat.SectorId);
                if (item == null)
                {
                    context.InterferenceStats.Add(stat);
                    context.SaveChanges();
                }
                else
                {
                    stat.UpdateInterferenceInfo(item);
                    stat.UpdateRtdInfo(item);
                    stat.UpdateTaInfo(item);
                }
            }
            context.SaveChanges();
        }

        public void Save(IEnumerable<PureInterferenceStat> stats)
        {
            foreach (PureInterferenceStat stat in stats)
            {
                PureInterferenceStat item = PureInterferenceStats.FirstOrDefault(x =>
                    x.CellId == stat.CellId && x.SectorId == stat.SectorId
                    && x.RecordDate == stat.RecordDate);
                if (item == null)
                {
                    context.PureInterferenceStats.Add(stat);
                    context.SaveChanges();
                }
            }
        }
    }
}
