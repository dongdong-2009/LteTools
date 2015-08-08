using System.Data.Entity;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Region.Concrete
{
    public class EFCollegeRepository : LightWeightRepositroyBase<CollegeInfo>, ICollegeRepository
    {
        protected override DbSet<CollegeInfo> Entities
        {
            get { return context.CollegeInfos; }
        }
    }

    public class EFInfrastructureRepository : IInfrastructureRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<InfrastructureInfo> InfrastructureInfos 
        { 
            get { return context.InfrastructureInfos.AsQueryable(); }
        }

        public void AddOneInfrastructure(InfrastructureInfo info)
        {
            context.InfrastructureInfos.Add(info);
        }

        public bool RemoveOneInfrastructure(InfrastructureInfo info)
        {
            return context.InfrastructureInfos.Remove(info) != null;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }

    public class EFIndoorDistributionRepository : IIndoorDistributioinRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<IndoorDistribution> IndoorDistributions 
        { 
            get { return context.IndoorDistributions.AsQueryable(); }
        }

        public IndoorDistribution AddOneDistribution(IndoorDistribution distributiion)
        {
            return context.IndoorDistributions.Add(distributiion);
        }

        public bool RemoveOneDistribution(IndoorDistribution distribution)
        {
            return context.IndoorDistributions.Remove(distribution) != null;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
