using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whc.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class UserTests : AuthenticatedTest
    {
        internal static User NewObject()
        {
            var obj = User.NewUser();
            obj.UserName = "test@test.com";
            obj.Password = "Password";
            obj.FirstName = "FirstName";
            obj.LastName = "Alternate";
            obj.IsDisabled = false;
            obj.Email = "test@test.com";
            return obj;
        }

        internal static User ExistingObject()
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

            var fetched = User.GetUser(obj.Id);

            AssertObjectsEqual(obj, fetched);
        }

        [TestMethod]
        public void Update()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = User.GetUser(obj.Id);

            fetched.UserName = "test@test2.com";
            fetched.Password = "Password2";
            fetched.IsDisabled = !fetched.IsDisabled;
            fetched.FirstName += "*";
            fetched.LastName += "*";
            fetched.Email = "test@test2.com";
            fetched = fetched.Save();

            var updated = User.GetUser(obj.Id);
            
            AssertObjectsEqual(fetched, updated);
        }

        [TestMethod]
        public void Update_Gets_RowVersion_From_Database()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = User.GetUser(obj.Id);
            fetched.FirstName = "FirstName2";
            fetched = fetched.Save();

            Assert.AreNotEqual(obj.RowVersion, fetched.RowVersion);
        }

        [TestMethod]
        public void Delete()
        {
            var obj = NewObject();
            obj = obj.Save();

            var fetched = User.GetUser(obj.Id);
            fetched.Delete();
            fetched = fetched.Save();

            try
            {
                var deleted = User.GetUser(obj.Id);
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
        public void UserName_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckEmailRequired(obj, x => x.UserName, true);
        }

        [TestMethod]
        public void UserName_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckEmailMaxLength(obj, x => x.UserName, 50);
        }

        [TestMethod]
        public void Email_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckEmailRequired(obj, x => x.Email, true);
        }

        [TestMethod]
        public void Email_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckEmailMaxLength(obj, x => x.Email, 50);
        }

        [TestMethod]
        public void Password_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckStringMaxLength(obj, x => x.Password, 50);
        }

        [TestMethod]
        public void Password_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckStringRequired(obj, x => x.Password, false);
        }

        [TestMethod]
        public void LastName_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckStringMaxLength(obj, x => x.LastName, 20);
        }

        [TestMethod]
        public void LastName_NotRequired()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckStringRequired(obj, x => x.LastName, false);
        }

        [TestMethod]
        public void FirstName_MaxLength()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckStringMaxLength(obj, x => x.FirstName, 20);
        }

        [TestMethod]
        public void FirstName_Required()
        {
            var obj = NewObject();
            Assert.IsTrue(obj.IsValid, "Object must be valid at the start of the test.");

            RuleValidator<User>.CheckStringRequired(obj, x => x.FirstName, true);
        }

        #endregion

        private void AssertObjectsEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.IsDisabled, actual.IsDisabled);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
        }
    }
}
