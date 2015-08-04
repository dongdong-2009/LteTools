using System.Linq;
using Lte.Parameters.Concrete;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Region.Concrete
{
    public class EFRegionRepository : IRegionRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<OptimizeRegion> OptimizeRegions
        {
            get
            {
                return context.OptimizeRegions;
            }
        }

        public void AddOneRegion(OptimizeRegion region)
        {
            context.OptimizeRegions.Add(region);
        }

        public void RemoveOneRegion(OptimizeRegion region)
        {
            context.OptimizeRegions.Remove(region);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }

    public class EFTownRepository : ITownRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<Town> Towns
        {
            get { return context.Towns; }
        }

        public void AddOneTown(Town town)
        {
            context.Towns.Add(town);
        }

        public bool RemoveOneTown(Town town)
        {
            return context.Towns.Remove(town) != null;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
