using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Domain.TypeDefs;
using Lte.Evaluations.Kpi;
using Lte.Parameters.Kpi.Abstract;
using Lte.WinApp.Models;
using Moq;

namespace Lte.WinApp.Test.Models
{
    public class StubFileInfoListImporter : FileInfoListImporter
    {
        public override string Import(ImportedFileInfo[] validFileInfos)
        {
            if (validFileInfos.Any())
            {
                return validFileInfos.Aggregate("", (current, info) => current + (info.FilePath + ","));
            }
            return "invalid";
        }
    }

    public class StubTimeStat : ITimeStat
    {
        public DateTime StatTime { get; set; }
    }

    public class FakeTopCellRepository : ITopCellRepository<StubTimeStat>
    {
        private readonly List<StubTimeStat> stats = new List<StubTimeStat>();
 
        public IQueryable<StubTimeStat> Stats {
            get { return stats.AsQueryable(); }
        }
        public void AddOneStat(StubTimeStat stat)
        {
            stats.Add(stat);
        }

        public void SaveChanges()
        {
            
        }
    }

    public class FakeStatDateImporter : IStatDateImporter
    {
        public DateTime Date { get; set; }
        public int ImportStat(StreamReader reader, CsvFileDescription fileDescriptionNamesUs)
        {
            return reader.ReadToEnd().Length;
        }
    }

    internal class FakeFileInfoListImporter : FileInfoListImporter<StubTimeStat, FakeTopCellRepository>
    {
        protected override IStatDateImporter GenerateImporter()
        {
            return new FakeStatDateImporter();
        }

        public FakeFileInfoListImporter(string contents)
        {
            ReadFile = x => contents.GetStreamReader();
        }
    }

    public class FakeStatDateImporterWithRepository : IStatDateImporter
    {
        public DateTime Date { get; set; }
        private readonly ITopCellRepository<StubTimeStat> _repository;
        public int ImportStat(StreamReader reader, CsvFileDescription fileDescriptionNamesUs)
        {
            _repository.AddOneStat(new StubTimeStat {StatTime = Date});
            return reader.ReadToEnd().Length;
        }

        public FakeStatDateImporterWithRepository(ITopCellRepository<StubTimeStat> repository)
        {
            _repository = repository;
        }
    }

    internal class FakeFileInfoListImporterWithRepository : FileInfoListImporter<StubTimeStat, FakeTopCellRepository>
    {
        protected override IStatDateImporter GenerateImporter()
        {
            return new FakeStatDateImporterWithRepository(repository);
        }

        public FakeFileInfoListImporterWithRepository(string contents)
        {
            ReadFile = x => contents.GetStreamReader();
        }
    }
}
