using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whc.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class InstrumentTests : AuthenticatedTest
    {
        internal static Instrument NewObject()
        {
            var obj = Instrument.NewInstrument();
            obj.Type = "Type";
            obj.NickName = "NickName";
            obj.SapPartType = InstrumentType.Fert;
            obj.SerialNumber = "SerialNumber";
            return obj;
        }

        internal static Instrument ExistingObject()
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
            Assert.AreNotEqual(0, obj.InstrumentId);
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

            var fetched = Instrument.GetInstrument(obj.InstrumentId);

            AssertObjectsEqual(obj, fetched);
        }

        [TestMethod]
        public void Update()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = Instrument.GetInstrument(obj.InstrumentId);

            fetched.NickName += "*";
            fetched.SapPartType = InstrumentType.Fert;
            fetched.SerialNumber += "*";
            fetched = fetched.Save();

            var updated = Instrument.GetInstrument(obj.InstrumentId);
            
            AssertObjectsEqual(fetched, updated);
        }

        [TestMethod]
        public void Update_Gets_RowVersion_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = Instrument.GetInstrument(obj.InstrumentId);
            fetched.SerialNumber = "SerialNumber2";
            fetched = fetched.Save();

            Assert.AreNotEqual(obj.RowVersion, fetched.RowVersion);
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
            obj.NickName += "*";
            obj = obj.Save();
            Assert.AreEqual(ModificationType.Update, obj.ModificationType);
        }


        [TestMethod]
        public void Delete()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = Instrument.GetInstrument(obj.InstrumentId);
            fetched.Delete();
            fetched = fetched.Save();

            try
            {
                var deleted = Instrument.GetInstrument(obj.InstrumentId);
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
        public void Type_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckStringRequired(obj, x => x.Type, true);
        }

        [TestMethod]
        public void Type_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckStringMaxLength(obj, x => x.Type, 200);
        }

        [TestMethod]
        public void NickName_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckStringRequired(obj, x => x.NickName, true);
        }

        [TestMethod]
        public void NickName_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckStringMaxLength(obj, x => x.NickName, 150);
        }

        [TestMethod]
        public void SapPartType_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckEnumRules(obj, x => x.SapPartType, InstrumentType.Fert, InstrumentType.None, true);
        }

        [TestMethod]
        public void SerialNumber_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckStringMaxLength(obj, x => x.SerialNumber, 100);
        }

        [TestMethod]
        public void SerialNumber_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<Instrument>.CheckStringRequired(obj, x => x.SerialNumber, true);
        }

        #endregion

        private void AssertObjectsEqual(Instrument expected, Instrument actual)
        {
            Assert.AreEqual(expected.InstrumentId, actual.InstrumentId);
            Assert.AreEqual(expected.NickName, actual.NickName);
            Assert.AreEqual(expected.SapPartType, actual.SapPartType);
            Assert.AreEqual(expected.SerialNumber, actual.SerialNumber);
        }
    }
}
