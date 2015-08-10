using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Service.Lte;
using Lte.WinApp.Service;

namespace Lte.WinApp.Models
{
    public abstract class FileInfoListImporter : IFileInfoListImporter
    {
        public List<ImportedFileInfo> FileInfoList { get; set; }

        public string FileType { get; set; }

        public abstract string Import(ImportedFileInfo[] validFileInfos);

        protected Func<string,StreamReader> ReadFile { get; set; }

        protected FileInfoListImporter()
        {
            ReadFile = x => new StreamReader(x, Encoding.GetEncoding("GB2312"));
        }
    }
    
    public class NeighborFileListImporter : FileInfoListImporter
    {
        private ILteNeighborCellRepository _repository;

        public NeighborFileListImporter(ILteNeighborCellRepository repository)
        {
            _repository = repository;
            FileType = "CSV-LTE邻区关系";
        }

        public override string Import(ImportedFileInfo[] validFileInfos)
        {
            string result = "";
            SaveLteCellRelationService service = new SaveLteCellRelationService(_repository);
            foreach (ImportedFileInfo info in validFileInfos)
            {
                using (StreamReader reader = ReadFile(info.FilePath))
                {
                    IEnumerable<LteCellRelationCsv> csvInfos =
                        CsvContext.Read<LteCellRelationCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                    service.Save(csvInfos);
                    result += "\n完成导入邻区关系文件：" + info.FilePath;
                }
            }
            return result;
        }
    }

}