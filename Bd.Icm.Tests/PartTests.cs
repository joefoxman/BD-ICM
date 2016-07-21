using System;
using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whc.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class PartTests : AuthenticatedTest
    {
        internal static Part NewObject()
        {
            var instrument = InstrumentTests.ExistingObject();
            var obj = Part.NewPart();
            obj.InstrumentId = instrument.InstrumentId;
            CreateValidData(obj);
            return obj;
        }
        private static void CreateValidData(Part obj)
        {
            obj.Name = "Name";
            obj.Description = "Description";
            obj.SapPartType = PartType.Halb;
            obj.SerialNumber = "SerialNumber";
            obj.DocumentNumber = "DocumentNumber";
            obj.DashNumber = 123;
            obj.RevisionNumber = 45;
            obj.MfgPartNumber = "MfgPartNumber";
            obj.LotCode = "LotCode";
            obj.DateCode = "DateCode";
            obj.SapPartNumber = "SapPartNumber";
            obj.Manufacturer = "Manufacturer";
        }

        internal static Part NewChildObject()
        {
            var obj = Part.NewChildPart();
            CreateValidData(obj);
            return obj;
        }

        internal static Part ExistingObject()
        {
            var obj = NewObject();
            return obj.Save();
        }

        [TestMethod]
        public void NewObjectIsValid()
        {
            var obj = NewObject();
            if (!obj.IsValid)
            {
                Assert.Fail("Object is not valid: " + obj.GetBrokenRules());
            }
        }

        [TestMethod]
        public void Insert()
        {
            var obj = NewObject();
            obj = obj.Save();
        }

        [TestMethod]
        public void InsertInstrumentChild()
        {
            var instrument = InstrumentTests.ExistingObject();
            var obj = NewChildObject();
            instrument.Parts.Add(obj);
            instrument = instrument.Save();
        }

        [TestMethod]
        public void InsertPartChild()
        {
            var part = ExistingObject();
            var obj = NewChildObject();
            part.Parts.Add(obj);
            part = part.Save();
        }

        [TestMethod]
        public void Insert_Gets_Identity_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();
            Assert.AreNotEqual(0, obj.Id);
        }

        [TestMethod]
        public void Insert_Gets_ModificationType_From_Repository()
        {
            var obj = NewObject();
            obj = obj.Save();
            Assert.AreEqual(ModificationType.Insert, obj.ModificationType);
        }

        [TestMethod]
        public void Update_Gets_ModificationType_From_Repository()
        {
            var obj = NewObject();
            obj = obj.Save();
            obj.Description += "*";
            obj = obj.Save();
            Assert.AreEqual(ModificationType.Update, obj.ModificationType);
        }

        [TestMethod]
        public void Update_Sets_InstrumentCommitId_To_Null()
        {
            var instrumentCommit = InstrumentCommitTests.ExistingObject();
            var obj = NewObject();
            obj.Commit(instrumentCommit.Id);
            obj = obj.Save();
            Assert.IsNotNull(obj.InstrumentCommitId);
            obj.Description += "*";
            obj = obj.Save();
            Assert.IsNull(obj.InstrumentCommitId);
        }

        [TestMethod]
        public void Delete_Gets_ModificationType_From_Repository()
        {
            var obj = NewObject();
            obj = obj.Save();
            obj.Delete();
            obj = obj.Save();
            Assert.AreEqual(ModificationType.Delete, obj.ModificationType);
        }

        [TestMethod]
        public void Delete_Sets_InstrumentCommitId_To_Null()
        {
            var instrumentCommit = InstrumentCommitTests.ExistingObject();
            var obj = NewObject();
            obj.Commit(instrumentCommit.Id);
            obj = obj.Save();
            Assert.IsNotNull(obj.InstrumentCommitId);
            obj.Delete();
            obj = obj.Save();
            Assert.IsNull(obj.InstrumentCommitId);
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

            var fetched = Part.GetPart(obj.Id);

            AssertObjectsEqual(obj, fetched);
        }

        [TestMethod]
        public void Update()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = Part.GetPart(obj.Id);

            fetched.Name += "*";
            fetched.SapPartType = PartType.Roh;
            fetched.SerialNumber += "*";
            fetched.Description += "*";
            fetched.DashNumber++;
            fetched.SapPartNumber += "*";
            fetched.LotCode += "*";
            fetched.DateCode += "*";
            fetched.Manufacturer += "*";
            fetched.MfgPartNumber += "*";
            fetched = fetched.Save();

            var updated = Part.GetPart(obj.Id);
            
            AssertObjectsEqual(fetched, updated);
        }

        [TestMethod]
        public void Update_Gets_RowVersion_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = Part.GetPart(obj.Id);
            fetched.SerialNumber = "SerialNumber2";
            fetched = fetched.Save();

            Assert.AreNotEqual(obj.RowVersion, fetched.RowVersion);
        }

        [TestMethod]
        public void Delete()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = Part.GetPart(obj.Id);
            fetched.Delete();
            fetched = fetched.Save();

            try
            {
                var deleted = Part.GetPart(obj.Id);
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
        public void Object_Invalid_If_No_ParentId()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            obj.ParentPartId = new int?();
            obj.InstrumentId = new int?();

            Assert.IsFalse(obj.IsValid, "Object should not be valid if not instrument and parent ID.");
        }

        [TestMethod]
        public void Object_Invalid_If_Both_ParentItemId_And_InstrumentId()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            obj.ParentPartId = 1;
            obj.InstrumentId = 1;

            Assert.IsFalse(obj.IsValid, "Object should not be valid if both instrument and parent ID provided.");
        }

        [TestMethod]
        public void Object_Valid_If_Only_ParentItemId()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            obj.ParentPartId = 1;
            obj.InstrumentId = new int?();

            Assert.IsTrue(obj.IsValid, "Object be valid if only parent ID provided.");
        }

        [TestMethod]
        public void Object_Valid_If_Only_InstrumentId()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            obj.ParentPartId = new int?();
            obj.InstrumentId = 1;

            Assert.IsTrue(obj.IsValid, "Object be valid if only parent ID provided.");
        }

        [TestMethod]
        public void Name_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.Name, true);
        }

        [TestMethod]
        public void Name_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.Name, 150);
        }

        [TestMethod]
        public void DocumentNumber_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.DocumentNumber, false);
        }

        [TestMethod]
        public void DocumentNumber_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.DocumentNumber, 50);
        }

        [TestMethod]
        public void SapPartNumber_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.SapPartNumber, false);
        }

        [TestMethod]
        public void SapPartNumber_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.SapPartNumber, 50);
        }

        [TestMethod]
        public void MfgPartNumber_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.MfgPartNumber, false);
        }

        [TestMethod]
        public void MfgPartNumber_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.MfgPartNumber, 50);
        }

        [TestMethod]
        public void Manufacturer_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.Manufacturer, false);
        }

        [TestMethod]
        public void Manufacturer_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.Manufacturer, 150);
        }

        [TestMethod]
        public void DateCode_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.DateCode, 200);
        }

        [TestMethod]
        public void SerialNumber_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.SerialNumber, 100);
        }

        [TestMethod]
        public void SerialNumber_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.SerialNumber, false);
        }

        [TestMethod]
        public void DashNumber_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckIntRequired(obj, x => x.DashNumber, false);
        }

        [TestMethod]
        public void RevisionNumber_MinMaxValue()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckIntMinMaxRules(obj, x => x.RevisionNumber, 1, int.MaxValue);
        }

        [TestMethod]
        public void LotCode_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringRequired(obj, x => x.LotCode, false);
        }

        [TestMethod]
        public void LotCode_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Part>.CheckStringMaxLength(obj, x => x.LotCode, 50);
        }

        #endregion

        private void AssertObjectsEqual(Part expected, Part actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.SapPartType, actual.SapPartType);
            Assert.AreEqual(expected.SerialNumber, actual.SerialNumber);
            Assert.AreEqual(expected.DateCode, actual.DateCode);
            Assert.AreEqual(expected.DashNumber, actual.DashNumber);
            Assert.AreEqual(expected.LotCode, actual.LotCode);
            Assert.AreEqual(expected.SapPartNumber, actual.SapPartNumber);
            Assert.AreEqual(expected.RevisionNumber, actual.RevisionNumber);
            Assert.AreEqual(expected.DocumentNumber, actual.DocumentNumber);
            Assert.AreEqual(expected.ParentPartId, actual.ParentPartId);
            Assert.AreEqual(expected.MfgPartNumber, actual.MfgPartNumber);
            Assert.AreEqual(expected.Manufacturer, actual.Manufacturer);
        }
    }
}
