namespace Lte.Domain.Geo.Abstract
{
    public interface ICell
    {
        int CellId { get; set; }

        byte SectorId { get; set; }
    }

    public interface ICdmaCell : ICell
    {
        int BtsId { get; set; }
    }

    public interface ITownId
    {
        int TownId { get; set; }
    }

    public interface ILteCell
    {
        int ENodebId { get; set; }

        byte SectorId { get; set; }
    }
}
