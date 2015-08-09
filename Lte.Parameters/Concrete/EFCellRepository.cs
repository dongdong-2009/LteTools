using System.Data.Entity;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCellRepository : LightWeightRepositroyBase<Cell>, ICellRepository
    {
        protected override DbSet<Cell> Entities
        {
            get { throw new System.NotImplementedException(); }
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
