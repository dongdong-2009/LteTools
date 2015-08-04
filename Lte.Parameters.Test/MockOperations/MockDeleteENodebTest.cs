using System;
using NUnit.Framework;
using Moq;
using Lte.Parameters.MockOperations;
using Lte.Parameters.Entities;
using Lte.Parameters.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Lte.Parameters.Test.MockOperations
{
    [TestFixture]
    public class MockDeleteENodebTest : MockENodebTestConfig
    {
        [SetUp]
        public void TestInitialize()
        {
            Initialize();
            eNodebRepository.MockENodebRepositoryDeleteENodeb(eNodebRepository.Object.GetAll());
        }

        [Test]
        public void TestInitialize_RemoveNullObject()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            eNodebRepository.Object.Delete((ENodeb)null);
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
        }

        [Test]
        public void TestInitialize_RemoveNewObject()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            eNodebRepository.Object.Delete(new ENodeb());
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
        }

        [Test]
        public void TestInitialize_RemoveFirstENodeb()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            eNodebRepository.Object.Delete(eNodebRepository.Object.GetAll().ElementAt(0));
            Assert.AreEqual(eNodebRepository.Object.Count(), 6);
        }

        [Test]
        public void TestInitialize_DeleteExistedENodebId()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            Assert.IsTrue(DeleteOneENodeb(1));
            Assert.AreEqual(eNodebRepository.Object.Count(), 6);
        }

        [Test]
        public void TestInitialize_DeleteInexistedENodebId()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7, "original");
            Assert.IsFalse(DeleteOneENodeb(-1));
            Assert.AreEqual(eNodebRepository.Object.Count(), 7, "after delete");
        }

        [Test]
        public void TestInitialize_DeleteENodeb_NullTownRepository()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            Assert.IsFalse(DeleteOneENodeb(null, "a", "b", "c", "d"));
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
        }

        [Test]
        public void TestInitialize_DeleteENodeb_InexistedTown()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            Assert.IsFalse(DeleteOneENodeb("a", "b", "c", "d"));
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
        }

        [Test]
        public void TestInitialize_DeleteENodeb_ExistedTown_InexistedENodebName()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7, "original");
            Assert.IsFalse(DeleteOneENodeb("C-1", "D-1", "T-1", "d"));
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
        }

        [Test]
        public void TestInitialize_DeleteENodeb_ExistedTown_ExistedENodebName()
        {
            Assert.AreEqual(eNodebRepository.Object.Count(), 7);
            Assert.IsTrue(DeleteOneENodeb("C-1", "D-1", "T-1", "E-1"));
            Assert.AreEqual(eNodebRepository.Object.Count(), 6);
        }
    }
}
