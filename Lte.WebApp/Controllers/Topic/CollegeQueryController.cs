using System;
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

        [HttpPost]
        public void PutPolygonArea(int id, double area, string message)
        {
            CollegeInfo info = _repository.Get(id);
            if (info == null) return;
            if (info.CollegeRegion == null)
            {
                info.CollegeRegion = new CollegeRegion
                {
                    Area = area,
                    Info = message,
                    RegionType = RegionType.Polygon
                };
            }
            else
            {
                info.CollegeRegion.Area = area;
                info.CollegeRegion.Info = message;
                info.CollegeRegion.RegionType = RegionType.Polygon;
            }
            _repository.Update(info);
        }

        [HttpPost]
        public void PutCircleArea(int id, double centerX, double centerY, double radius)
        {
            CollegeInfo info = _repository.Get(id);
            if (info==null) return;
            double area = Math.PI*radius*radius;
            string message = centerX + ";" + centerY + ";" + radius;
            if (info.CollegeRegion == null)
            {
                info.CollegeRegion = new CollegeRegion
                {
                    Area = area,
                    Info = message,
                    RegionType = RegionType.Circle
                };
            }
            else
            {
                info.CollegeRegion.Area = area;
                info.CollegeRegion.Info = message;
                info.CollegeRegion.RegionType = RegionType.Circle;
            }
            _repository.Update(info);
        }

        [HttpPost]
        public void PutRectangleArea(int id, double x1, double y1, double x2, double y2, double area)
        {
            CollegeInfo info = _repository.Get(id);
            if (info == null) return;
            string message = x1 + ";" + y1 + ";" + x2 + ";" + y2;
            if (info.CollegeRegion == null)
            {
                info.CollegeRegion = new CollegeRegion
                {
                    Area = area,
                    Info = message,
                    RegionType = RegionType.Rectangle
                };
            }
            else
            {
                info.CollegeRegion.Area = area;
                info.CollegeRegion.Info = message;
                info.CollegeRegion.RegionType = RegionType.Rectangle;
            }
            _repository.Update(info);
        }
    }
}
