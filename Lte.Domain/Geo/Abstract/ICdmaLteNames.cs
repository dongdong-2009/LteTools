namespace Lte.Domain.Geo.Abstract
{
    public interface ICdmaLteNames
    {
        int ENodebId { get; set; }

        int CdmaCellId { get; set; }

        string CdmaName { get; set; }

        string LteName { get; set; }

        byte SectorId { get; set; }
    }
}
