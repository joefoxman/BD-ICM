using Bd.Icm.DataAccess;
using Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whc.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class InstrumentCommitTests : AuthenticatedTest
    {
        internal static InstrumentCommit NewObject()
        {
            var parent = InstrumentTests.ExistingObject();
            var obj = InstrumentCommit.NewInstrumentCommit();
            obj.InstrumentId = parent.InstrumentId;
            obj.Notes = "Notes";
            obj.Revision = 1;
            obj.EffectiveTo = 1;
            return obj;
        }

        internal static InstrumentCommit ExistingObject()
        {
            var obj = NewObject();
            return obj.Save();
        }

        [TestMethod]
        public void Insert()
        {
            var obj = NewObject();
            obj = obj.Save();
        }

        [TestMethod]
        public void Insert_Gets_Identity_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();
            Assert.AreNotEqual(0, obj.Id);
        }

        [TestMethod]
        public void Insert_Gets_RowVersion_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();
            Assert.AreNotEqual(0, obj.RowVersion);
        }

        [TestMethod]
        public void Fetch()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = InstrumentCommit.GetInstrumentCommit(obj.Id);

            AssertObjectsEqual(obj, fetched);
        }

        [TestMethod]
        public void Update()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = InstrumentCommit.GetInstrumentCommit(obj.Id);

            fetched.Notes += "*";
            fetched.Revision++;
            fetched.EffectiveTo++;
            fetched = fetched.Save();

            var updated = InstrumentCommit.GetInstrumentCommit(obj.Id);

            AssertObjectsEqual(fetched, updated);
        }

        [TestMethod]
        public void Update_Gets_RowVersion_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = InstrumentCommit.GetInstrumentCommit(obj.Id);
            fetched.Notes += "*";
            fetched = fetched.Save();

            Assert.AreNotEqual(obj.RowVersion, fetched.RowVersion);
        }

        [TestMethod]
        public void Delete()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = InstrumentCommit.GetInstrumentCommit(obj.Id);
            fetched.Delete();
            fetched = fetched.Save();

            try
            {
                var deleted = InstrumentCommit.GetInstrumentCommit(obj.Id);
            }
            catch (DataPortalException dpex)
            {
                if (dpex.BusinessException is RecordNotFoundException)
                    return;
            }

            Assert.Fail("Record not deleted.");
        }

        #region [Business Rule Checks]

        [TestMethod]
        public void Notes_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<InstrumentCommit>.CheckEmailRequired(obj, x => x.Notes, false);
        }

        [TestMethod]
        public void Notes_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<InstrumentCommit>.CheckEmailMaxLength(obj, x => x.Notes, 255);
        }

        [TestMethod]
        public void Revision_Minvalue()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<InstrumentCommit>.CheckIntMinMaxRules(obj, x => x.Revision, 0, int.MaxValue);
        }

        #endregion

        private void AssertObjectsEqual(InstrumentCommit expected, InstrumentCommit actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.InstrumentId, actual.InstrumentId);
            Assert.AreEqual(expected.Revision, actual.Revision);
            Assert.AreEqual(expected.EffectiveTo, actual.EffectiveTo);
        }
    }
}
