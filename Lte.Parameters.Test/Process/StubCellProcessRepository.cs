using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Entities;
using Lte.Parameters.Abstract;

namespace Lte.Parameters.Test.Process
{
    public class StubCellProcessRepository : ICellRepository
    {
        public IQueryable<Cell> Cells 
        {
            get
            {
                return new List<Cell>(){
                    new Cell(){
                        ENodebId = 1,
                        SectorId = 2
                    }
                }.AsQueryable();
            }
        }

        public int CurrentProgress { get; set; }

        public StubCellProcessRepository()
        {
            CurrentProgress = 0;
        }

        public void AddOneCell(Cell cell)
        {
            CurrentProgress += 5;
        }

        public void AddCells(IEnumerable<Cell> cells)
        {
            CurrentProgress += 10;
        }

        public bool SaveCell(CellExcel cellInfo, IENodebRepository eNodebRepository)
        {
            return true;
        }

        public int SaveCells(List<CellExcel> cellInfoList, IENodebRepository eNodebRepository)
        {
            if (CurrentProgress > 10) { CurrentProgress = 0; }
            return CurrentProgress++;
        }

        public bool RemoveOneCell(Cell cell)
        {
            return true;
        }

        public bool DeleteCell(int eNodebId, byte sectorId)
        {
            return true;
        }

        public void SaveChanges() { }
    }
}
