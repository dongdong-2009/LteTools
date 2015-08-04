using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.WinApp.Models;
using Moq;

namespace Lte.WinApp.Test.Service
{
    public class ImportFileInfoTestConfig
    {
        protected List<ImportedFileInfo> fileInfoList;
        protected Mock<IFileInfoListImporter> importer = new Mock<IFileInfoListImporter>();

        protected void Initialize()
        {
            fileInfoList = new List<ImportedFileInfo>
            {
                new ImportedFileInfo {FilePath = "path1", IsSelected = false, FileType = ""},
                new ImportedFileInfo {FilePath = "path2", IsSelected = false, FileType = ""},
                new ImportedFileInfo {FilePath = "path3", IsSelected = false, FileType = ""}
            };
            importer.SetupGet(x => x.FileType).Returns("txt");
            importer.SetupGet(x => x.FileInfoList).Returns(fileInfoList);
        }
    }
}
