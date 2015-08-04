using System;
using Lte.Domain.Geo.Abstract;

namespace Lte.Parameters.Abstract
{
    public interface ILogRecord : ILteCell
    {
        double Rsrp { get; set; }

        double Sinr { get; set; }

        double AverageCqi { get; set; }

        short UlMcs { get; set; }

        short DlMcs { get; set; }

        int UlThroughput { get; set; }

        int DlThroughput { get; set; }

        int PuschRbRate { get; set; }

        int PdschRbRate { get; set; }

        DateTime Time { get; set; }

        short Pci { get; set; }

        int Earfcn { get; set; }

        int PdschTbCode0 { get; set; }

        int PdschTbCode1 { get; set; }
    }
}
