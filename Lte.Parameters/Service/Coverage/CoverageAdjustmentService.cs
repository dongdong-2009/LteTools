using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Service.Coverage
{
    public class CoverageAdjustmentService
    {
        private readonly ICoverageRepository _repository;

        public CoverageAdjustmentService(ICoverageRepository repository)
        {
            _repository = repository;
        }

        public CoverageAdjustment QueryItem(CoverageAdjustment adjustment)
        {
            return _repository.CoverageAdjustments.FirstOrDefault(
                x => x.ENodebId == adjustment.ENodebId && x.SectorId == adjustment.SectorId
                    && x.Frequency == adjustment.Frequency);
        }
    }
}
