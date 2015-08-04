using System.Linq;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.LinqToCsv.Mapper;
using Lte.Domain.Test.LinqToCsv.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.Domain.Test.LinqToCsv.TestFieldMapper
{
    [TestClass]
    public class FieldMapperConstructorTest
    {
        private FieldMapper<ProductData> productMapper;

        [TestMethod]
        public void TestConstructDefault_WritingFile()
        {
            productMapper = new FieldMapper<ProductData>(new CsvFileDescription(), "test04.csv", true);
            Assert.AreEqual(productMapper.NameToInfo.Count, 11);
            Assert.AreEqual(productMapper.NameToInfo.ElementAt(0).Key, "Name");
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[0].CanBeNull, true);
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo.Count(), 11);
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[1].HasColumnAttribute, true);
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[2].Name, "LaunchTime");
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[3].Index, 4);
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[4].MemberInfo.MemberType.ToString(), "Property");
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[5].OutputFormat, "G");
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[5].Name, "Code");
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[6].MemberInfo.Name, "RetailPrice");
            Assert.AreEqual(productMapper.FieldIndexInfo.IndexToInfo[7].CanBeNull, true);
        }
    }
}
