using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Domain.TypeDefs;
using Lte.Evaluations.Kpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Abstract;
using Lte.Parameters.Service.Lte;
using Lte.WinApp.Service;

namespace Lte.WinApp.Models
{
    public interface IFileInfoListImporter
    {
        List<ImportedFileInfo> FileInfoList { get; set; }

        string FileType { get; set; }

        void Import(IEnumerable<string> fileNames);

        string Import(ImportedFileInfo[] validFileInfos);
    }

    public abstract class FileInfoListImporter : IFileInfoListImporter
    {
        public List<ImportedFileInfo> FileInfoList { get; set; }

        public string FileType { get; set; }

        public void Import(IEnumerable<string> fileNames)
        {
            ImportFileInfoService service = new ImportFileInfoService(this, fileNames);
            service.Import();
        }

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

    public abstract class FileInfoListImporter<TStat, TRepository> : FileInfoListImporter
        where TRepository : class, ITopCellRepository<TStat>, new()
        where TStat : class, ITimeStat
    {
        protected ITopCellRepository<TStat> repository;

        protected abstract IStatDateImporter GenerateImporter();

        public override string Import(ImportedFileInfo[] validFileInfos)
        {
            string result = "";
            repository = new TRepository();

            for (int i=0; i<validFileInfos.Length; i++)
            {
                IStatDateImporter importer = GenerateImporter();
                ImportedFileInfo fileInfo = validFileInfos[i];
                importer.Date = fileInfo.FilePath.RetrieveFileNameBody().GetDateExtend();
                TStat stat = repository.Stats.FirstOrDefault(x => x.StatTime == importer.Date);
                if (stat == null)
                {
                    using (StreamReader reader = ReadFile(fileInfo.FilePath))
                    {
                        int count = importer.ImportStat(reader, CsvFileDescription.CommaDescription);
                        result += "\n" + fileInfo.FilePath + "完成导入数量：" + count;
                    }
                    fileInfo.FinishState();
                }
                else
                {
                    result += "\n日期：" + importer.Date.ToShortDateString() + "的统计记录已导入！";
                    fileInfo.UnnecessaryState();
                }
            }
            return result;
        }
    }
}
