using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Service.Public;
using Lte.WinApp.Models;
using Lte.WinApp.Service;

namespace Lte.WinApp.ViewPages
{
    /// <summary>
    /// ParametersImportPage.xaml 的交互逻辑
    /// </summary>
    public partial class ParametersImportPage : Page
    {
        private readonly List<ImportedFileInfo> _fileInfoList = new List<ImportedFileInfo>();
        private readonly LteFileInfoListImporter _lteImporter;
        private readonly CdmaFileInfoListImporter _cdmaImporter;
        private readonly MmlFileInfoListImporter _mmlImporter;
        private readonly ParametersDumpInfrastructure infrastructure = new ParametersDumpInfrastructure();

        private readonly ParametersDumpConfig dumpConfig = new ParametersDumpConfig
        {
            ImportBts = true,
            ImportCdmaCell = true,
            ImportENodeb = true,
            ImportLteCell = true,
            UpdateCdmaCell = false,
            UpdateLteCell = false,
            UpdatePci = false,
            UpdateBts = false,
            UpdateENodeb = false
        };

        public ParametersImportPage()
        {
            InitializeComponent();
            PageTitle.Content = Title;
            _lteImporter = new LteFileInfoListImporter
            {
                FileInfoList = _fileInfoList
            };
            infrastructure.LteENodebRepository = _lteImporter;
            infrastructure.LteCellRepository = _lteImporter;
            _cdmaImporter = new CdmaFileInfoListImporter
            {
                FileInfoList = _fileInfoList
            };
            infrastructure.CdmaBtsRepository = _cdmaImporter;
            infrastructure.CdmaCellRepository = _cdmaImporter;
            _mmlImporter = new MmlFileInfoListImporter
            {
                FileInfoList = _fileInfoList
            };
            DumpConfig.DataContext = dumpConfig;
        }

        private void ReadLte_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWrapper wrapper = new OpenLteFileDialogWrapper();
            if (wrapper.ShowDialog())
            {
                _lteImporter.ImportFiles(wrapper.FileNames);
                FileList.SetDataSource(_fileInfoList);
            }
        }

        private void ReadCdma_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWrapper wrapper = new OpenCdmaFileDialogWrapper();
            if (wrapper.ShowDialog())
            {
                _cdmaImporter.ImportFiles(wrapper.FileNames);
                FileList.SetDataSource(_fileInfoList);
            }
        }

        private void ReadMmls_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWrapper wrapper = new OpenMmlFileDialogWrapper();
            if (wrapper.ShowDialog())
            {
                _mmlImporter.ImportFiles(wrapper.FileNames);
                infrastructure.MmlRepositoryList = _mmlImporter.RepositoryList;
                FileList.SetDataSource(_fileInfoList);
            }
        }

        private void ImportFiles_Click(object sender, RoutedEventArgs e)
        {
            ImportedFileInfo[] validLteFiles = _lteImporter.Query().ToArray();
            ImportedFileInfo[] validCdmaFiles = _cdmaImporter.Query().ToArray();
            ImportedFileInfo[] validMmlFiles = _mmlImporter.Query().ToArray();
            if (validCdmaFiles.Length + validLteFiles.Length + validMmlFiles.Length == 0)
            {
                MessageBox.Show("未选择任何有效的数据文件。请先导入或选择！");
                return;
            }
            if (validLteFiles.Any())
            {
                _lteImporter.Import(validLteFiles);
                ENodebList.SetDataSource(_lteImporter.BtsExcelList);
                CellList.SetDataSource(_lteImporter.CellExcelList);
            }
            if (validCdmaFiles.Any())
            {
                _cdmaImporter.Import(validCdmaFiles);
                BtsList.SetDataSource(_cdmaImporter.BtsExcelList);
                CdmaCellList.SetDataSource(_cdmaImporter.CellExcelList);
            }
            if (validMmlFiles.Any())
            {
                _mmlImporter.Import(validMmlFiles);
            }
            FileList.SetDataSource(_fileInfoList);
        }

        private async void DumpToDb_Click(object sender, RoutedEventArgs e)
        {
            WinDumpController controller = new WinDumpController();
            ParametersDumpGenerator generater = new ParametersDumpGenerator();
            await Task.Run(() =>
            {
                generater.DumpLteData(infrastructure, controller, dumpConfig, DumpResults);
                generater.DumpMmlData(infrastructure, controller);
                generater.DumpCdmaData(infrastructure, controller, dumpConfig, DumpResults);
                MessageBox.Show("新增LTE基站：" + infrastructure.ENodebsUpdated +
                                "\n新增LTE小区：" + infrastructure.CellsInserted +
                                "\n更新LTE小区：" + infrastructure.CellsUpdated +
                                "\n更新LTE邻区PCI：" + infrastructure.NeighborPciUpdated +
                                "\n新增CDMA基站：" + infrastructure.CdmaBtsUpdated +
                                "\n新增CDMA小区：" + infrastructure.CdmaCellsInserted +
                                "\n更新CDMA小区：" + infrastructure.CdmaCellsUpdated,
                    "执行结果");
            });
        }
    }
}
