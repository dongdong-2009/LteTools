using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Regular;

namespace Lte.Domain.Test.Regular
{
    [TestClass]
    public class GetStreamReaderTest
    {

        [TestMethod]
        public void TestStreamReaderFromString()
        {
            string input =
@"Id,Name,Last Name,Age,City
1,John,Doe,15,Washington";

            StreamReader sReader = input.GetStreamReader();
            Assert.IsNotNull(sReader);
        }

    }
}
