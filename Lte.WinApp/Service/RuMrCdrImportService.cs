using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.WinApp.Models;

namespace Lte.WinApp.Service
{
    public static class RuMrCdrImportService
    {
        public static string ImportRu(this IFileInfoListImporter ruImporter)
        {
            QueryValidFileInfosService service = new QueryValidFileInfosService(ruImporter);
            ImportedFileInfo[] validRuFileInfos = service.Query().ToArray();
            return validRuFileInfos.Any() ? ruImporter.Import(validRuFileInfos) : "";
        }

        public static string ImportMr(this IFileInfoListImporter mrImporter)
        {
            QueryValidFileInfosService service = new QueryValidFileInfosService(mrImporter);
            ImportedFileInfo[] validMrFileInfos = service.Query().ToArray();
            return validMrFileInfos.Any() ? mrImporter.Import(validMrFileInfos) : "";
        }

        public static string ImportCdr(this IFileInfoListImporter cdrImporter)
        {
            QueryValidFileInfosService service = new QueryValidFileInfosService(cdrImporter);
            ImportedFileInfo[] validCdrFileInfos = service.Query().ToArray();
            return validCdrFileInfos.Any() ? cdrImporter.Import(validCdrFileInfos) : "";
        }
    }
}
