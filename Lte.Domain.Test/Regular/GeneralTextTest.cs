using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.Regular;

namespace Lte.Domain.Test.Regular
{
    [TestClass]
    public class GeneralTextTest
    {
        [TestMethod]
        public void TestGetSubStringInFirstBracket_OneBracket()
        {
            string line = "123(456)234";
            Assert.AreEqual(line.GetSubStringInFirstBracket(), "456");

        }

        [TestMethod]
        public void TestGetSubStringInFirstBracket_TwoBrackets()
        {
            string line = "12(34)56(789)0";
            Assert.AreEqual(line.GetSubStringInFirstBracket(), "34");
        }

        [TestMethod]
        public void TestGetSubStringInFirstBracket_OnlyFirstHalfBracket()
        {
            string line = "12(34567";
            Assert.AreEqual(line.GetSubStringInFirstBracket(), "34567");
        }

        [TestMethod]
        public void TestGetSubStringInFirstBracket_OnlySecondHalfBracket()
        {
            string line = "12345)67";
            Assert.AreEqual(line.GetSubStringInFirstBracket(), "12345");
        }

        [TestMethod]
        public void TestGetSubStringInFirstBracket_NoBrackets()
        {
            string line = "12345";
            Assert.AreEqual(line.GetSubStringInFirstBracket(), "12345");
        }
    }
}
