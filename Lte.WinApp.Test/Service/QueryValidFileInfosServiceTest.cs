using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.WinApp.Service;
using NUnit.Framework;

namespace Lte.WinApp.Test.Service
{
    [TestFixture]
    public class QueryValidFileInfosServiceTest : ImportFileInfoTestConfig
    {
        private QueryValidFileInfosService service;

        [SetUp]
        public void SetUp()
        {
            Initialize();
            for (int i = 0; i < 3; i++)
            {
                fileInfoList[i].FileType = "txt";
            }
            service = new QueryValidFileInfosService(importer.Object);
        }

        private int GetFileInfoListLength()
        {
            return service.Query().Count();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestQuery_ModifyOneFileType(int index)
        {
            Assert.AreEqual(GetFileInfoListLength(), 3);
            fileInfoList[index].FileType = "bat";
            Assert.AreEqual(GetFileInfoListLength(), 2);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestQuery_ModifyOneFileStat(int index)
        {
            Assert.AreEqual(GetFileInfoListLength(), 3);
            fileInfoList[index].FinishState();
            Assert.AreEqual(GetFileInfoListLength(), 2);
        }
    }
}
