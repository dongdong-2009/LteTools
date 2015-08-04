using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ICoverageRepository
    {
        IQueryable<CoverageAdjustment> CoverageAdjustments { get; }

        void Save(IEnumerable<CoverageAdjustment> adjustments);
    }

    public interface IInterferenceStatRepository
    {
        IQueryable<InterferenceStat> InterferenceStats { get; }

        IQueryable<PureInterferenceStat> PureInterferenceStats { get; }

        void Save(IEnumerable<InterferenceStat> stats);

        void Save(IEnumerable<PureInterferenceStat> stats);
    }

}
