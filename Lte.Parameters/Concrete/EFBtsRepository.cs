using System.Linq;
using Abp.EntityFramework;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFBtsRepository : ParametersRepositoryBase<CdmaBts>, IBtsRepository
    {
        public EFBtsRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public EFBtsRepository() : this(new EFParametersProvider())
        {
        }
    }

    public class EFENodebPhotoRepository : IENodebPhotoRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<ENodebPhoto> Photos 
        { 
            get { return context.ENodebPhotos; }
        }

        public void AddOnePhoto(ENodebPhoto photo)
        {
            context.ENodebPhotos.Add(photo);
        }

        public bool RemoveOnePhoto(ENodebPhoto photo)
        {
            return (context.ENodebPhotos.Remove(photo) != null);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
