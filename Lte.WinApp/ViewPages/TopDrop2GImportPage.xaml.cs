using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Lte.Parameters.Concrete;
using Lte.WinApp.Models;
using Lte.WinApp.Service;
using MessageBox = System.Windows.MessageBox;

namespace Lte.WinApp.ViewPages
{
    /// <summary>
    /// TopDrop2GImportPage.xaml 的交互逻辑
    /// </summary>
    public partial class TopDrop2GImportPage
    {
        private readonly List<ImportedFileInfo> _fileInfoList = new List<ImportedFileInfo>();
        private readonly IFileInfoListImporter _kpiImporter;
        private readonly IFileInfoListImporter _preciseImporter;
        private readonly IFileInfoListImporter _neighborImporter;
        private readonly QueryValidFileInfosService _kpiService;
        private readonly QueryValidFileInfosService _preciseService;
        private readonly QueryValidFileInfosService _neighborService;

        public TopDrop2GImportPage()
        {
            InitializeComponent();
            PageTitle.Content = Title;
            _kpiImporter = new KpiFileInfoListImporter(1000) { FileInfoList = _fileInfoList };
            _preciseImporter = new Precise4GFileInfoListImporter {FileInfoList = _fileInfoList};
            _neighborImporter = new NeighborFileListImporter(new EFLteNeighborCellRepository())
            {
                FileInfoList = _fileInfoList
            };
            _kpiService = new QueryValidFileInfosService(_kpiImporter);
            _preciseService = new QueryValidFileInfosService(_preciseImporter);
            _neighborService = new QueryValidFileInfosService(_neighborImporter);
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            ImportedFileInfo[] validKpiFileInfos = _kpiService.Query().ToArray();
            ImportedFileInfo[] validPreciseFileInfos = _preciseService.Query().ToArray();
            ImportedFileInfo[] validNeighborFileInfos = _neighborService.Query().ToArray();
            if (validPreciseFileInfos.Length + validKpiFileInfos.Length 
                + validNeighborFileInfos.Length == 0)
            {
                MessageBox.Show("未选择任何有效的CSV文件。请先导入或选择！");
                return;
            }
            string result = "";
            if (validKpiFileInfos.Any())
            {
                result += _kpiImporter.Import(validKpiFileInfos);
            }

            if (validPreciseFileInfos.Any())
            {
                result += _preciseImporter.Import(validPreciseFileInfos);
            }

            if (validNeighborFileInfos.Any())
            {
                result += _neighborImporter.Import(validNeighborFileInfos);
            }

            MessageBox.Show(result);
            FileList.SetDataSource(_fileInfoList);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWrapper wrapper = new OpenKpiFileDialogWrapper();
            if (wrapper.ShowDialog())
            {
                _kpiImporter.Import(wrapper.FileNames);
                FileList.SetDataSource(_fileInfoList);
            }
        }

        private void OpenPreciseFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWrapper wrapper = new OpenPreciseFileDialogWrapper();
            if (wrapper.ShowDialog())
            {
                _preciseImporter.Import(wrapper.FileNames);
                FileList.SetDataSource(_fileInfoList);
            }
        }

        private void OpenNeighborFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWrapper wrapper = new OpenLteNeighborFileDialogWrapper();
            if (wrapper.ShowDialog())
            {
                _neighborImporter.Import(wrapper.FileNames);
                FileList.SetDataSource(_fileInfoList);
            }
        }
    }
}
