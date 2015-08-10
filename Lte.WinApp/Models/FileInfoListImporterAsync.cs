using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Domain.TypeDefs;
using Lte.Evaluations.Kpi;
using Lte.Parameters.Kpi.Abstract;
using Lte.WinApp.Service;

namespace Lte.WinApp.Models
{
    public abstract class FileInfoListImporterAsync<TStat, TRepository> : IFileInfoListImporterAsync
        where TRepository : class, ITopCellRepository<TStat>, new()
        where TStat : class, ITimeStat
    {
        protected ITopCellRepository<TStat> repository;

        protected abstract IStatDateImporter GenerateImporter();

        protected Func<string, StreamReader> ReadFile { get; set; }

        public List<ImportedFileInfo> FileInfoList { get; set; }

        public string FileType { get; protected set; }

        public string Result { get; private set; }

        public async void Import(ImportedFileInfo[] validFileInfos)
        {
            Result = "";
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
                        int count = await importer.ImportStat(reader, CsvFileDescription.CommaDescription);
                        Result += "\n" + fileInfo.FilePath + "完成导入数量：" + count;
                    }
                    fileInfo.FinishState();
                }
                else
                {
                    Result += "\n日期：" + importer.Date.ToShortDateString() + "的统计记录已导入！";
                    fileInfo.UnnecessaryState();
                }
            }
        }
    }
}
