using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFMrsCellRepository : IMrsCellRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<MrsCellDate> MrsCells
        {
            get { return context.MrsCells.AsQueryable(); }
        }

        public IQueryable<MrsCellTa> TaCells
        {
            get { return context.MrsTaCells.AsQueryable(); }
        }

        public void AddOneCell(MrsCellDate cell)
        {
            context.MrsCells.Add(cell);
        }

        public void AddOneCell(MrsCellTa cell)
        {
            context.MrsTaCells.Add(cell);
        }

        public bool RemoveOneCell(MrsCellDate cell)
        {
            return context.MrsCells.Remove(cell) != null;
        }

        public bool RemoveOneCell(MrsCellTa cell)
        {
            return context.MrsTaCells.Remove(cell) != null;
        }

        public void SaveChanges()
        {
            context.SaveChangesWithDelay();
        }
    }

    public class EFMroCellRepository : IMroCellRepository
    {
        private readonly EFParametersContext context = new EFParametersContext();

        public IQueryable<MroRsrpTa> RsrpTaCells
        {
            get { return context.MroRsrpTaCells.AsQueryable(); }
        }

        public void AddOneCell(MroRsrpTa cell)
        {
            context.MroRsrpTaCells.Add(cell);
        }

        public bool RemovceOneCell(MroRsrpTa cell)
        {
            return context.MroRsrpTaCells.Remove(cell) != null;
        }

        public void SaveChanges()
        {
            context.SaveChangesWithDelay();
        }
    }
}