using System;
using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whc.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class PartActionTests : AuthenticatedTest
    {
        internal static PartAction NewObject()
        {
            var part = PartTests.ExistingObject();
            var obj = PartAction.NewPartAction();
            obj.Action = PartActionType.Calibration;
            obj.ActionDate = DateTime.Now.Date;
            obj.Description = "Description";
            part.Actions.Add(obj);
            return obj;
        }

        internal static Part ExistingObject()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            return parent;
        }

        [TestMethod]
        public void Insert()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
        }

        [TestMethod]
        public void Insert_Gets_Identity_From_Database()
        {
            var parent = ExistingObject();
            parent = Part.GetPart(parent.Id);
            var obj = parent.Actions[0];
            Assert.AreNotEqual(0, obj.Id);
        }

        [TestMethod]
        public void Insert_Gets_RowVersion_From_Database()
        {
            var parent = ExistingObject();
            parent = Part.GetPart(parent.Id);
            var obj = parent.Actions[0];
            Assert.AreNotEqual(0, obj.RowVersion);
        }

        [TestMethod]
        public void Fetch()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            obj = parent.Actions[0];
            parent = Part.GetPart(parent.Id);
            var fetched = parent.Actions[0];

            AssertObjectsEqual(obj, fetched);
        }

        [TestMethod]
        public void Update()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            parent = Part.GetPart(parent.Id);
            var fetched = parent.Actions[0];

            fetched.Action = PartActionType.Repair;
            fetched.ActionDate = fetched.ActionDate.AddDays(-1);
            fetched.Description += "*";

            parent = parent.Save();

            parent = Part.GetPart(parent.Id);
            var updated = parent.Actions[0];

            AssertObjectsEqual(fetched, updated);
        }

        [TestMethod]
        public void Update_Gets_RowVersion_From_Database()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            parent = Part.GetPart(parent.Id);
            var fetched = parent.Actions[0];
            fetched.Description += "*";
            parent = parent.Save();

            parent = Part.GetPart(parent.Id);
            var updated = parent.Actions[0];

            Assert.AreNotEqual(obj.RowVersion, updated.RowVersion);
        }

        [TestMethod]
        public void Insert_Gets_ModificationType_From_Repository()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            obj = parent.Actions[0];
            Assert.AreEqual(ModificationType.Insert, obj.ModificationType);
        }

        [TestMethod]
        public void Update_Gets_ModificationType_From_Repository()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            obj = parent.Actions[0];
            obj.Description += "*";
            parent = parent.Save();
            obj = parent.Actions[0];
            Assert.AreEqual(ModificationType.Update, obj.ModificationType);
        }

        [TestMethod]
        public void Deleted_When_Parent_Deleted()
        {
            var parent = PartTests.ExistingObject();
            var obj = NewObject();
            parent.Actions.Add(obj);
            parent = parent.Save();
            parent.Delete();
            parent = parent.Save();

            using (var repository = new Repository<DataAccess.Database.PartAction>())
            {
                var found = repository.FirstOrDefault(x => x.Id == obj.Id);
                if (found != null)
                    Assert.Fail("Record not deleted.");
            }
        }

        #region [Business Rule Checks]

        [TestMethod]
        public void Description_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartAction>.CheckStringRequired(obj, x => x.Description, false);
        }

        [TestMethod]
        public void Description_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartAction>.CheckStringMaxLength(obj, x => x.Description, 300);
        }

        [TestMethod]
        public void Action_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<PartAction>.CheckEnumRules<PartActionType>(obj, x => x.Action, PartActionType.Calibration, PartActionType.None, true);
        }

        #endregion

        private void AssertObjectsEqual(PartAction expected, PartAction actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.ActionDate.ToString(), actual.ActionDate.ToString());
            Assert.AreEqual(expected.Action, actual.Action);
        }
    }
}
