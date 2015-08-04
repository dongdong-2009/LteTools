using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.Domain.Test.Regular
{
    [TestClass]
    public class ClassDeriveTest
    {
        [TestMethod]
        public void TestClassDerive()
        {
            AB ab = new AB();
            Assert.IsFalse(ab is IAB);
        }

        [TestMethod]
        public void TestAggregate()
        {
            IEnumerable<string> source = new[] {"1", "2", "3"};
            string result = source.Aggregate((x, y) => x + ',' + y);
            Assert.AreEqual(result, "1,2,3");
        }

        public interface IA
        {
            string OutputA();
        }

        public interface IB
        {
            string OutputB();
        }

        public interface IAB : IA, IB
        { }

        public class A : IA
        {
            public string OutputA()
            { return "A"; }
        }

        public class AB : A, IB
        {
            public string OutputB()
            { return "B"; }
        }
    }
}
