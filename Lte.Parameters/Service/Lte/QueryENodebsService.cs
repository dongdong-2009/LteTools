using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Lte.Domain.Geo.Abstract;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Region.Service;

namespace Lte.Parameters.Service.Lte
{
    public static class QueryENodebService
    {
        public static IEnumerable<ENodeb> QueryWithNames(this IRepository<ENodeb> repository,
            ITownRepository townRepository, string city, string district, string town, string eNodebName,
            string address)
        {
            IEnumerable<Town> _townList = townRepository.Towns.QueryTowns(city, district, town).ToList();
            return (!_townList.Any())
                ? null
                : repository.GetAllList().Where(x =>
                    _townList.FirstOrDefault(t => t.Id == x.TownId) != null
                    && (string.IsNullOrEmpty(eNodebName) || x.Name.IndexOf(eNodebName.Trim(),
                        StringComparison.Ordinal) >= 0)
                    && (string.IsNullOrEmpty(address) || x.Address.IndexOf(address.Trim(),
                        StringComparison.Ordinal) >= 0));
        }
    }

    public class EFENodebRepository : ParametersRepositoryBase<ENodeb>, IENodebRepository
    {
        private EFENodebRepository(IDbContextProvider<EFParametersContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public EFENodebRepository()
            : this(new EFParametersProvider())
        {
        }

        public List<ENodeb> GetAllWithIds(IEnumerable<int> ids)
        {
            return (from a in GetAll()
                   join b in ids on a.ENodebId equals b
                   select a).OrderBy(x=>x.ENodebId).ToList();
        }

        public List<ENodeb> GetAllWithNames(ITownRepository townRepository, string city, string district, string town, string eNodebName,
            string address)
        {
            return this.QueryWithNames(townRepository, city, district, town, eNodebName, address).ToList();
        }

        public List<ENodeb> GetAllWithNames(ITownRepository townRepository, ITown town, string eNodebName, string address)
        {
            return GetAllWithNames(townRepository, town.CityName, town.DistrictName, town.TownName,
                eNodebName, address);
        }
    }

}
