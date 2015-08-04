using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Abp.Domain.Repositories;
using Lte.Domain.Geo.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;

namespace Lte.Parameters.Abstract
{
    public interface IENodebRepository : IRepository<ENodeb>
    {
        List<ENodeb> GetAllWithIds(IEnumerable<int> ids);

        List<ENodeb> GetAllWithNames(ITownRepository townRepository, string city, string district, string town,
            string eNodebName, string address);

        List<ENodeb> GetAllWithNames(ITownRepository townRepository, ITown town, string eNodebName, string address);
    }

    public interface IBtsRepository
    {
        IQueryable<CdmaBts> Btss { get; }

        void AddOneBts(CdmaBts bts);

        bool RemoveOneBts(CdmaBts bts);

        void SaveChanges();
    }

    public interface IENodebPhotoRepository
    {
        IQueryable<ENodebPhoto> Photos { get; }

        void AddOnePhoto(ENodebPhoto photo);

        bool RemoveOnePhoto(ENodebPhoto photo);

        void SaveChanges();
    }
}
