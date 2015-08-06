using System.Linq;
using Abp.Domain.Repositories;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Region.Abstract
{
    public interface IRegionRepository
    {
        IQueryable<OptimizeRegion> OptimizeRegions { get; }

        void AddOneRegion(OptimizeRegion region);

        void RemoveOneRegion(OptimizeRegion region);

        void SaveChanges();
    }

    public interface ITownRepository
    {
        IQueryable<Town> Towns { get; }

        void AddOneTown(Town town);

        bool RemoveOneTown(Town town);

        void SaveChanges();
    }

    public interface ICollegeRepository : IRepository<CollegeInfo>
    {
        void SaveChanges();
    }
}
