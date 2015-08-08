using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class ClassDeriveTest
    {
        [Test]
        public void TestClassDerive()
        {
            AB ab = new AB();
            Assert.IsFalse(false);
        }

        [Test]
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
