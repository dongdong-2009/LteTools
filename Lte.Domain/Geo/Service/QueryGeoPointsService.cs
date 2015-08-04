using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Measure;

namespace Lte.Domain.Geo.Service
{
    public class QueryGeoPointsService<TInPoint, TOutPoint>
        where TInPoint : IGeoPoint<double>
        where TOutPoint : class, IGeoPoint<double>, new()
    {
        private readonly IEnumerable<TInPoint> cellList;

        public QueryGeoPointsService(IEnumerable<TInPoint> cellList)
        {
            this.cellList = cellList;
        }

        public List<TOutPoint> Query(double degreeSpan, double degreeInterval)
        {
            double minLattitute = cellList.Select(x => x.Lattitute).Min() - degreeSpan;
            double maxLattitute = cellList.Select(x => x.Lattitute).Max() + degreeSpan;
            List<TOutPoint> tempPointList = new List<TOutPoint>();
            for (double lattitute = minLattitute; lattitute <= maxLattitute; lattitute += degreeInterval)
            {
                IEnumerable<TInPoint> subCellList = cellList.Where(x =>
                    lattitute - degreeSpan <= x.Lattitute && x.Lattitute <= lattitute + degreeSpan);
                if (!subCellList.Any()) continue;
                double minLongtitute = subCellList.Min(x => x.Longtitute) - degreeSpan;
                double maxLongtitute = subCellList.Max(x => x.Longtitute) + degreeSpan;
                for (double longtitute = minLongtitute; longtitute <= maxLongtitute; longtitute += degreeInterval)
                {
                    tempPointList.Add(new TOutPoint { Longtitute = longtitute, Lattitute = lattitute });
                }
            }
            return tempPointList;
        }
    }
}
