using System.Collections.Generic;
using System.Linq;
using Lte.WinApp.Models;

namespace Lte.WinApp.Service
{
    public class QueryValidFileInfosService
    {
        private readonly IFileInfoListImporter _importer;

        public QueryValidFileInfosService(IFileInfoListImporter importer)
        {
            _importer = importer;
        }

        public IEnumerable<ImportedFileInfo> Query()
        {
            return !_importer.FileInfoList.Any() ? _importer.FileInfoList
                : _importer.FileInfoList.Where(x => 
                    x.FileType == _importer.FileType && x.CurrentState == "未读取");
        }
    }
}
