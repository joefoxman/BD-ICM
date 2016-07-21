﻿using System.Linq;
using Bd.Icm.DataAccess.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WHC.UnitTesting.MSTest;

namespace Bd.Icm.DataAccess.Tests
{
    [TestClass]
    public class PartVersionRepositoryTests : TransactionalTest
    {
        private int GetNewId()
        {
            using (var repo = new PartVersionRepository())
            {
                return repo.GetNewId(1);
            }
        }

        [TestMethod]
        public void GetNewId_IdGenerated()
        {
            var newId = GetNewId();
            Assert.AreNotEqual(0, newId);
        }

        [TestMethod]
        public void GetNewId_CreatedBySet()
        {
            var newId = GetNewId();
            using (var entities = new BdIcmEntities())
            {
                var idRecord = entities.PartVersions.Single(x => x.PartId == newId);
                Assert.AreEqual(1, idRecord.CreatedBy);
            }
        }

        [TestMethod]
        public void GetNewId_ModifiedBySet()
        {
            var newId = GetNewId();
            using (var entities = new BdIcmEntities())
            {
                var idRecord = entities.PartVersions.Single(x => x.PartId == newId);
                Assert.AreEqual(1, idRecord.ModifiedBy);
            }
        }
    }
}
