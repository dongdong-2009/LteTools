using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lte.Parameters.Service.Public;

namespace Lte.WinApp.Controls
{

    /// <summary>
    /// ParametersDumpResults.xaml 的交互逻辑
    /// </summary>
    public partial class ParametersDumpResults : UserControl, IParametersDumpResults
    {
        public ParametersDumpResults()
        {
            InitializeComponent();
            ENodebUpdated.Content = 0;
            CellInserted.Content = 0;
            CellUpdated.Content = 0;
            NeighborPciUpdated.Content = 0;
            CdmaBtsUpdated.Content = 0;
            CdmaCellInserted.Content = 0;
            CdmaCellUpdated.Content = 0;
        }

        public int ENodebs { set { ENodebUpdated.Content = value; }}
        public int NewCells{set { CellInserted.Content = value; }}
        public int UpdateCells{set { CellUpdated.Content = value; }}
        public int UpdatePcis{set { NeighborPciUpdated.Content = value; }}
        public int CdmaBts{set { CdmaBtsUpdated.Content = value; }}
        public int NewCdmaCells { set { CdmaCellInserted.Content = value; } }
        public int UpdateCdmaCells{set { CdmaCellUpdated.Content = value; }}
    }
}
