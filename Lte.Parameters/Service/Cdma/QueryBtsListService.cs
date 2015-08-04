using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Lte.Parameters.Region.Service;

namespace Lte.Parameters.Service.Cdma
{
    public class QueryBtsListService
    {
        private readonly IBtsRepository _repository;
        private readonly IEnumerable<Town> _townList;
        private readonly string _btsName;
        private readonly string _address;

        public QueryBtsListService(IBtsRepository repository,
            IEnumerable<Town> townList, string btsName, string address)
        {
            _repository = repository;
            _townList = townList;
            _btsName = btsName;
            _address = address;
        }

        public QueryBtsListService(IBtsRepository repository,
            ITownRepository townRepository, string district, string town,
            string btsName, string address)
            : this(repository, null, btsName, address)
        {
            List<Town> towns = townRepository.Towns.QueryTowns(district, town).ToList();
            if (towns.Any())
            {
                _townList = towns;
            }
        }

        public IEnumerable<CdmaBts> Query()
        {
            return _repository.Btss.ToList().Where(x =>
                _townList.FirstOrDefault(t => t.Id == x.TownId) != null
                && (string.IsNullOrEmpty(_btsName) || x.Name.IndexOf(_btsName.Trim(), StringComparison.Ordinal) >= 0)
                && (string.IsNullOrEmpty(_address) || x.Address.IndexOf(_address.Trim(), StringComparison.Ordinal) >= 0));
        }
    }
}
