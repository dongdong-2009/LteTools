using System.Linq;
using System.Web.Http;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.WebApp.Controllers.Topic
{
    public class CollegeQueryController : ApiController
    {
        private readonly ICollegeRepository _repository;

        public CollegeQueryController(ICollegeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public CollegeInfo Get(int id)
        {
            return _repository.Get(id);
        }

        public void PutCircleArea(int id, double centerX)
        {
        }
    }
}
