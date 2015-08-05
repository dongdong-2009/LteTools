using System.Linq;
using Lte.Domain.Geo.Abstract;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Parameters.Region.Service
{
    public class TownOperationService
    {
        private readonly ITownRepository _repository;
        private readonly string _city;
        private readonly string _district;
        private readonly string _town;

        public TownOperationService(ITownRepository townRepository,
            string cityName, string districtName, string townName)
        {
            _repository = townRepository;
            _city = cityName;
            _district = districtName;
            _town = townName;
        }

        public TownOperationService(ITownRepository townRepository, ITown town)
            : this(townRepository, town.CityName, town.DistrictName, town.TownName)
        {
        }

        public void SaveOneTown()
        {
            Town town = _repository.Towns.ToList().Query(_city, _district, _town);
            if (town != null) return;
            _repository.AddOneTown(new Town
            {
                CityName = _city.Trim(),
                DistrictName = _district.Trim(),
                TownName = _town.Trim()
            });
            _repository.SaveChanges();
        }

        public bool DeleteOneTown()
        {
            Town town = _repository.Towns.ToList().Query(_city.Trim(), _district.Trim(), _town.Trim());
            if (town == null) return false;
            bool result = _repository.RemoveOneTown(town);
            _repository.SaveChanges();
            return result;
        }

        public bool DeleteOneTown(IENodebRepository eNodebRepository, IBtsRepository btsRepository)
        {
            bool result = false;
            Town town = _repository.Towns.ToList().Query(_city.Trim(), _district.Trim(), _town.Trim());

            if (town == null) return false;
            ENodeb eNodeb
                = (eNodebRepository != null && eNodebRepository.GetAll() != null) ?
                    eNodebRepository.GetAll().FirstOrDefault(x => x.TownId == town.Id) :
                    null;
            CdmaBts bts
                = (btsRepository != null && btsRepository.GetAll() != null)
                    ? btsRepository.GetAll().FirstOrDefault(x => x.TownId == town.Id)
                    : null;
            if (eNodeb == null && bts == null)
            {
                result = _repository.RemoveOneTown(town);
                _repository.SaveChanges();
            }
            return result;
        }
    }
}
