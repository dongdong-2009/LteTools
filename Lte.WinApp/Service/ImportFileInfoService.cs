using System.Collections.Generic;
using System.Linq;
using Lte.WinApp.Models;

namespace Lte.WinApp.Service
{
    public class ImportFileInfoService
    {
        private readonly IFileInfoListImporter _importer;
        private readonly IEnumerable<string> _fileNames;

        public ImportFileInfoService(IFileInfoListImporter importer, IEnumerable<string> fileNames)
        {
            _importer = importer;
            _fileNames = fileNames;
        }

        public void Import()
        {
            foreach (string fileName in _fileNames.Where(
                fileName => _importer.FileInfoList.All(x => x.FilePath != fileName)))
            {
                _importer.FileInfoList.Add(new ImportedFileInfo
                {
                    FilePath = fileName,
                    FileType = _importer.FileType,
                    IsSelected = true
                });
            }
        }
    }
}
