using Abp.Domain.Repositories;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Region.Abstract
{
    public interface IRegionRepository : IRepository<OptimizeRegion>
    {
    }

    public interface ITownRepository : IRepository<Town>
    {
    }

    public interface ICollegeRepository : IRepository<CollegeInfo>
    {
        CollegeRegion GetRegion(int id);
    }
}
