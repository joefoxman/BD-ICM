using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whc.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class PartMetadataTests : AuthenticatedTest
    {
        internal static PartMetadata NewObject()
        {
            var part = PartTests.ExistingObject();
            var obj = PartMetadata.NewPartMetadata();
            obj.MetaKey = "MetaKey";
            obj.MetaValue = "MetaValue";
            part.Metadata.Add(obj);
            return obj;
        }

        internal static Part ExistingObject()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            return parent;
        }

        [TestMethod]
        public void Insert()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
        }

        [TestMethod]
        public void Insert_Gets_Identity_From_Database()
        {
            var parent = ExistingObject();
            parent = Part.GetPart(parent.Id);
            var obj = parent.Metadata[0];
            Assert.AreNotEqual(0, obj.Id);
        }

        [TestMethod]
        public void Insert_Gets_RowVersion_From_Database()
        {
            var parent = ExistingObject();
            parent = Part.GetPart(parent.Id);
            var obj = parent.Metadata[0];
            Assert.AreNotEqual(0, obj.RowVersion);
        }

        [TestMethod]
        public void Fetch()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            obj = parent.Metadata[0];
            parent = Part.GetPart(parent.Id);
            var fetched = parent.Metadata[0];

            AssertObjectsEqual(obj, fetched);
        }

        [TestMethod]
        public void Update()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            parent = Part.GetPart(parent.Id);
            var fetched = parent.Metadata[0];

            fetched.MetaKey += "*";
            fetched.MetaValue += "*";

            parent = parent.Save();

            parent = Part.GetPart(parent.Id);
            var updated = parent.Metadata[0];

            AssertObjectsEqual(fetched, updated);
        }

        [TestMethod]
        public void Update_Gets_RowVersion_From_Database()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            parent = Part.GetPart(parent.Id);
            var fetched = parent.Metadata[0];
            fetched.MetaValue += "*";
            parent = parent.Save();

            parent = Part.GetPart(parent.Id);
            var updated = parent.Metadata[0];

            Assert.AreNotEqual(obj.RowVersion, updated.RowVersion);
        }

        [TestMethod]
        public void Insert_Gets_ModificationType_From_Repository()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            obj = parent.Metadata[0];
            Assert.AreEqual(ModificationType.Insert, obj.ModificationType);
        }

        [TestMethod]
        public void Update_Gets_ModificationType_From_Repository()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            obj = parent.Metadata[0];
            obj.MetaValue += "*";
            parent = parent.Save();
            obj = parent.Metadata[0];
            Assert.AreEqual(ModificationType.Update, obj.ModificationType);
        }

        [TestMethod]
        public void Deleted_When_Parent_Deleted()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Metadata.Add(obj);
            parent = parent.Save();
            parent.Delete();
            parent = parent.Save();

            using (var repository = new Repository<DataAccess.Database.PartMetadata>())
            {
                var found = repository.FirstOrDefault(x => x.Id == obj.Id);
                if (found != null)
                    Assert.Fail("Record not deleted.");
            }
        }

        #region [Business Rule Checks]

        [TestMethod]
        public void MetaKey_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartMetadata>.CheckStringRequired(obj, x => x.MetaKey, true);
        }

        [TestMethod]
        public void MetaKey_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartMetadata>.CheckStringMaxLength(obj, x => x.MetaKey, 50);
        }

        [TestMethod]
        public void MetaValue_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartMetadata>.CheckStringRequired(obj, x => x.MetaValue, true);
        }

        [TestMethod]
        public void MetaValue_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartMetadata>.CheckStringMaxLength(obj, x => x.MetaValue, 50);
        }
        #endregion

        private void AssertObjectsEqual(PartMetadata expected, PartMetadata actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.MetaKey, actual.MetaKey);
            Assert.AreEqual(expected.MetaValue, actual.MetaValue);
        }
    }
}
