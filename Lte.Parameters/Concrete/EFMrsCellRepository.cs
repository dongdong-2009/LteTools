using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFMrsCellRepository : LightWeightRepositroyBase<MrsCellDate>, IMrsCellRepository
    {
        protected override DbSet<MrsCellDate> Entities
        {
            get { return context.MrsCells; }
        }
    }

    public class EFMrsCellTaRepository : LightWeightRepositroyBase<MrsCellTa>, IMrsCellTaRepository
    {
        protected override DbSet<MrsCellTa> Entities
        {
            get { return context.MrsTaCells; }
        }
    }

    public class EFMroCellRepository : LightWeightRepositroyBase<MroRsrpTa>, IMroCellRepository
    {
        protected override DbSet<MroRsrpTa> Entities
        {
            get { return context.MroRsrpTaCells; }
        }
    }
}