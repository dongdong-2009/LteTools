using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCellRepository : ICellRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<Cell> Cells
        {
            get { return context.Cells; }
        }

        public void AddOneCell(Cell cell)
        {
            context.Cells.Add(cell);
        }

        public bool RemoveOneCell(Cell cell)
        {
            return (context.Cells.Remove(cell) != null);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }

    public class EFCdmaCellRepository : ICdmaCellRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<CdmaCell> Cells
        {
            get { return context.CdmaCells.AsQueryable(); }
        }

        public void AddOneCell(CdmaCell cell)
        {
            context.CdmaCells.Add(cell);
        }

        public bool RemoveOneCell(CdmaCell cell)
        {
            return (context.CdmaCells.Remove(cell) != null);
        }

        public void SaveChanges()
        {
            context.SaveChangesWithDelay();
        }
    }

    public class EFLteNeighborCellRepository : ILteNeighborCellRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<LteNeighborCell> NeighborCells
        {
            get { return context.LteNeighborCells.AsQueryable(); }
        }

        public IQueryable<NearestPciCell> NearestPciCells 
        { 
            get { return context.NearestPciCells.AsQueryable(); }
        }

        public void AddOneCell(LteNeighborCell cell)
        {
            context.LteNeighborCells.Add(cell);
        }

        public bool RemoveOneCell(LteNeighborCell cell)
        {
            return (context.LteNeighborCells.Remove(cell) != null);
        }

        public void SaveChanges()
        {
            context.SaveChangesWithDelay();
        }
    }
}
