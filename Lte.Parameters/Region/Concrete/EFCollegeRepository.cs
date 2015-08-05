using System.Linq;
using Abp.EntityFramework;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Region.Concrete
{
    public class EFCollegeRepository : ParametersRepositoryBase<CollegeInfo>, ICollegeRepository
    {
        public EFCollegeRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public EFCollegeRepository() : this(new EFParametersProvider())
        {
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
