using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.TypeDefs
{
    [TestFixture]
    public class LegalIpTest
    {
        [Test]
        public void TestLegalIp_AllZeros_Legal()
        {
            const string address = "0.0.0.0";
            Assert.IsTrue(address.IsLegalIp());
        }

        [Test]
        public void TestLegalIp_AllOnes_Legal()
        {
            const string address = "1.1.1.1";
            Assert.IsTrue(address.IsLegalIp());
        }

        [Test]
        public void TestLegalIp_All127_Legal()
        {
            const string address = "127.127.127.127";
            Assert.IsTrue(address.IsLegalIp());
        }

        [Test]
        public void TestLegalIp_All255_Legal()
        {
            const string address = "255.255.255.255";
            Assert.IsTrue(address.IsLegalIp());
        }

        [Test]
        public void TestLegalIp_LocalMachine_Legal()
        {
            const string address = "127.0.0.1";
            Assert.IsTrue(address.IsLegalIp());
        }

        [TestCase("10.17.165.100")]
        [TestCase("10.17.0.128")]
        [TestCase("10.16.128.33")]
        [TestCase("172.16.165.100")]
        [TestCase("172.154.1.255")]
        [TestCase("172.183.128.201")]
        [TestCase("192.168.202.177")]
        [TestCase("192.170.1.23")]
        [TestCase("192.183.22.201")]
        [TestCase("224.0.0.2")]
        [TestCase("224.2.1.211")]
        [TestCase("224.0.35.21")]
        public void TestLegalIp_Type_Legal(string address)
        {
            Assert.IsTrue(address.IsLegalIp());
        }

        [TestCase("12.33.45.2.1")]
        [TestCase("0.0.12.37.2.9")]
        [TestCase("233.67.90.87.56")]
        public void TestLegalIp_TooManyDigits_Illegal(string address )
        {
            Assert.IsFalse(address.IsLegalIp());
        }

        [TestCase("12.33.45")]
        [TestCase("0.0")]
        [TestCase("233.67")]
        public void TestLegalIp_TooLessDigits_Illegal(string address)
        {
            Assert.IsFalse(address.IsLegalIp());
        }

        [TestCase("12.33.45.256")]
        [TestCase("1000.0.12.37")]
        [TestCase("301.67.90.87")]
        [TestCase("12.331.45.252")]
        [TestCase("1001.0.12.371")]
        [TestCase("30.67.900.87")]
        public void TestLegalIp_TooLargeDigits_Illegal(string address)
        {
            Assert.IsFalse(address.IsLegalIp());
        }

        [TestCase("12a.33.45.2e1")]
        [TestCase("0-0.12.37.2")]
        [TestCase("233.6_7.90.87.56")]
        public void TestLegalIp_OtherChars_Illegal(string address)
        {
            Assert.IsFalse(address.IsLegalIp());
        }
    }
}
