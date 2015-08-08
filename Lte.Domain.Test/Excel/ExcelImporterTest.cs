using Lte.Domain.Regular;
using Ninject;
using Ninject.Modules;
using Ninject.Activation;
using NUnit.Framework;

namespace Lte.Domain.Test.Excel
{
    [TestFixture]
    public class ExcelImporterTest
    {
        [Test]
        public void TestExcelImporter()
        {
            ExcelImporterService service;
            var kernel = new StandardKernel(new ExcelImporterModule());
            service = kernel.Get<ExcelImporterService>();

            IExcelImporter importer = service.Importer;
            Assert.IsNotNull(importer["基站级"]);
            Assert.IsNotNull(importer["小区级"]);
            Assert.AreEqual(importer["基站级"].TableName, "基站级");
        }
    }

    public abstract class ExcelImporterService
    {
        readonly IExcelImporter importer;

        public IExcelImporter Importer
        {
            get { return importer; }
        }

        protected ExcelImporterService(IExcelImporter importer)
        {
            this.importer = importer;
        }

    }

    public class ExcelImporterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IExcelImporter>().ToProvider(new SheetNamesComponentPorvider());
        }
    }

    public class SheetNamesComponentPorvider : Provider<IExcelImporter>
    {
        protected override IExcelImporter CreateInstance(IContext context)
        {
            string[] sheetNames = { "基站级", "小区级" };
            return new StubExcelImporter(sheetNames);
        }
    }

}
