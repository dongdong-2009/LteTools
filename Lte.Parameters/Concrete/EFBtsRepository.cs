using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFBtsRepository : IBtsRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<CdmaBts> Btss
        {
            get { return context.Btss.AsQueryable(); }
        }

        public void AddOneBts(CdmaBts bts)
        {
            context.Btss.Add(bts);
        }

        public bool RemoveOneBts(CdmaBts bts)
        {
            return (context.Btss.Remove(bts) != null);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
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
