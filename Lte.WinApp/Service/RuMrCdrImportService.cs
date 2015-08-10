using System.Linq;
using Lte.WinApp.Models;

namespace Lte.WinApp.Service
{
    public static class RuMrCdrImportService
    {
        public static string ImportRu(this IFileInfoListImporter ruImporter)
        {
            ImportedFileInfo[] validRuFileInfos = ruImporter.Query().ToArray();
            return validRuFileInfos.Any() ? ruImporter.Import(validRuFileInfos) : "";
        }

        public static string ImportMr(this IFileInfoListImporter mrImporter)
        {
            ImportedFileInfo[] validMrFileInfos = mrImporter.Query().ToArray();
            return validMrFileInfos.Any() ? mrImporter.Import(validMrFileInfos) : "";
        }

        public static string ImportCdr(this IFileInfoListImporter cdrImporter)
        {
            ImportedFileInfo[] validCdrFileInfos = cdrImporter.Query().ToArray();
            return validCdrFileInfos.Any() ? cdrImporter.Import(validCdrFileInfos) : "";
        }
    }
}
