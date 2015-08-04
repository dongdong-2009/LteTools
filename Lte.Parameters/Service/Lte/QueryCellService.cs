using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Service.Lte
{
    public class QueryCellService
    {
        private readonly ICellRepository _repository;

        public QueryCellService(ICellRepository repository)
        {
            _repository = repository;
        }

        private bool Delete(Cell cell)
        {
            bool result = (cell != null) && _repository.RemoveOneCell(cell);
            if (!result) return false;
            _repository.SaveChanges();
            return true;
        }

        public bool Delete(int eNodebId, byte sectorId)
        {
            return Delete(_repository.Cells.FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId));
        }
    }

    public class QueryMrsCellService : IDisposable
    {
        private readonly IMrsCellRepository _repository;

        public QueryMrsCellService(IMrsCellRepository repository)
        {
            _repository = repository;
        }

        public void SaveStats(IEnumerable<MrsCellDate> stats)
        {
            foreach (MrsCellDate stat in 
                from stat in stats let item = _repository.MrsCells.FirstOrDefault(x =>
                x.RecordDate == stat.RecordDate && x.CellId == stat.CellId && x.SectorId == stat.SectorId) 
                where item == null select stat)
            {
                stat.UpdateStats();
                _repository.AddOneCell(stat);
            }
            _repository.SaveChanges();
        }

        public void SaveTaStats(IEnumerable<MrsCellTa> stats)
        {
            foreach (MrsCellTa stat in
                from stat in stats
                let item = _repository.TaCells.FirstOrDefault(x =>
                    x.RecordDate == stat.RecordDate && x.CellId == stat.CellId && x.SectorId == stat.SectorId)
                where item == null
                select stat)
            {
                stat.UpdateStats();
                _repository.AddOneCell(stat);
            }
            _repository.SaveChanges();
        }

        public void Dispose()
        {
        }
    }

    public class QueryMroCellService : IDisposable
    {
        private readonly IMroCellRepository _repository;

        public QueryMroCellService(IMroCellRepository repository)
        {
            _repository = repository;
        }

        public void SaveRsrpTaStats(IEnumerable<MroRsrpTa> stats)
        {
            foreach (MroRsrpTa stat in 
                from stat in stats
                let item = _repository.RsrpTaCells.FirstOrDefault(x =>
                    x.RecordDate == stat.RecordDate && x.CellId == stat.CellId && x.SectorId == stat.SectorId)
                where item == null
                select stat)
            {
                _repository.AddOneCell(stat);
            }
            _repository.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
