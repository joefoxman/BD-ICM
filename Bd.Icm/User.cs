using System.Globalization;
using Csla;
using Csla.Rules.CommonRules;
using System;
using System.Linq;
using System.Security.Principal;
using System.Security.Cryptography;
using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Bd.Icm.Rules;
using Csla.Security;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class User : DataObject<User>,
        IIdentity
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);
        public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(c => c.Password);
        public static readonly PropertyInfo<bool> IsDisabledProperty = RegisterProperty<bool>(c => c.IsDisabled);
        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);
        public static readonly PropertyInfo<UserRoles> RolesProperty = RegisterProperty<UserRoles>(c => c.Roles);

        public UserRoles Roles => GetProperty(RolesProperty);

        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            set { SetProperty(LastNameProperty, value); }
        }

        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            set { SetProperty(FirstNameProperty, value); }
        }

        public bool IsDisabled
        {
            get { return GetProperty(IsDisabledProperty); }
            set { SetProperty(IsDisabledProperty, value); }
        }

        public string Password
        {
            get { return GetProperty(PasswordProperty); }
            set { SetProperty(PasswordProperty, value); }
        }

        public string UserName
        {
            get { return GetProperty(UserNameProperty); }
            set { SetProperty(UserNameProperty, value); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static User Current => GetUser(ApplicationContext.User.Identity.Name);

        public static string HashPassword(string password) {
            var x = new MD5CryptoServiceProvider();
            var bs = System.Text.Encoding.UTF8.GetBytes(password);
            bs = x.ComputeHash(bs);
            var s = new System.Text.StringBuilder();
            foreach (var b in bs) {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public static string HashPasswordSha256(string password)
        {
            using (var hasher = new SHA256Managed())
            {
                var plainTextBytes = new byte[password.Length];
                var hashedBytes = hasher.ComputeHash(plainTextBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static string CreateTemporaryPassword(int length)
        {
            Func<Random, char> randomNumber = rnd => (char)rnd.Next(48, 58);
            Func<Random, char> randonCharacter = rnd => (char)rnd.Next(97, 123);

            var funcArray = new[] { randomNumber, randonCharacter };
            var chars = new char[length];
            var random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, funcArray.Length);
                chars[i] = funcArray[index](random);
            }
            return new string(chars);
        }

        public bool IsInRole(string role)
        {
            RoleType roleType;
            return Enum.TryParse(role, true, out roleType) && Roles.Any(x => x.Role == roleType);
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new Required(UserNameProperty));
            BusinessRules.AddRule(new MaxLength(UserNameProperty, 50));

            //BusinessRules.AddRule(new Required(PasswordProperty));
            BusinessRules.AddRule(new MaxLength(PasswordProperty, 50));

            BusinessRules.AddRule(new Required(FirstNameProperty));
            BusinessRules.AddRule(new MaxLength(FirstNameProperty, 20));

            BusinessRules.AddRule(new MaxLength(LastNameProperty, 20));

            BusinessRules.AddRule(new RegExMatch(EmailProperty, RegExPatterns.EmailAddress));
            BusinessRules.AddRule(new MaxLength(EmailProperty, 50));
        }

        #endregion

        #region [Factory Methods]

        public static User NewUser()
        {
            return DataPortal.Create<User>();
        }

        public static User GetUser(int id)
        {
            return DataPortal.Fetch<User>(id);
        }

        public static User GetUser(string username)
        {
            return DataPortal.Fetch<User>(username);
        }

        public static User GetUserByResetToken(string token)
        {
            return DataPortal.Fetch<User>(new Guid(token));
        }

        public static User ValidateLogin(string username, string password)
        {
            return DataPortal.Fetch<User>(new UsernameCriteria(username, password));
        }

        public static void DeleteUser(int id)
        {
            DataPortal.Delete<User>(id);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        protected override void DataPortal_Create()
        {
            LoadProperty(RolesProperty, UserRoles.NewUserRoles());
        }

        [UsedImplicitly]
        private void DataPortal_Fetch(int id)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                var data = repository.SingleOrDefault(x => x.Id == id);
                if (data == null)
                    throw new RecordNotFoundException("User", id.ToString(CultureInfo.InvariantCulture), "Record not found.");

                ImportData(data);
            }
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void DataPortal_Fetch(string username)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                var data = repository.SingleOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (data == null)
                    throw new RecordNotFoundException("User", username, "Record not found.");

                ImportData(data);
            }
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void DataPortal_Fetch(UsernameCriteria criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                var hashedPassword = string.IsNullOrWhiteSpace(criteria.Password) ? "" : HashPassword(criteria.Password);
                var data = repository.Fetch().Where(x => x.UserName.Equals(criteria.Username, StringComparison.OrdinalIgnoreCase) && x.Password == hashedPassword);
                if (!data.Any())
                    throw new AuthenticationException();
                ImportData(data.First());
            }
            BusinessRules.CheckRules();
        }

        protected override void DataPortal_Insert()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                repository.Insert(data);
                LoadProperty(IdProperty, data.Id);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
            }

            FieldManager.UpdateChildren(this);
        }

        protected override void DataPortal_Update()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                repository.Update(data);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
            }
            FieldManager.UpdateChildren(this);
        }

        protected override void DataPortal_DeleteSelf()
        {
            Roles.Clear();
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                repository.Delete(ExportData());
            }
        }

        [UsedImplicitly]
        private void DataPortal_Delete(SingleCriteria<User, int> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                var data = repository.First(x => x.Id == criteria.Value);
                repository.Delete(data);
            }
        }

        #endregion

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.User data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(UserNameProperty, data.UserName);
            LoadProperty(PasswordProperty, data.Password);
            LoadProperty(EmailProperty, data.Email);
            LoadProperty(FirstNameProperty, data.FirstName);
            LoadProperty(LastNameProperty, data.LastName);
            LoadProperty(IsDisabledProperty, data.IsDisabled);
            LoadProperty(RolesProperty, UserRoles.GetUserRoles(data.UserRoles));
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
        }

        private DataAccess.Database.User ExportData()
        {
            SetDataStamps();

            var data = new DataAccess.Database.User
            {
                Password = ReadProperty(PasswordProperty),
                Id = ReadProperty(IdProperty),
                UserName = ReadProperty(UserNameProperty),
                Email = ReadProperty(EmailProperty),
                FirstName = ReadProperty(FirstNameProperty),
                LastName = ReadProperty(LastNameProperty),
                IsDisabled = ReadProperty(IsDisabledProperty),
                CreatedBy = ReadProperty(CreatedByProperty),
                CreatedDate = ReadProperty(CreatedDateProperty),
                ModifiedBy = ReadProperty(ModifiedByProperty),
                ModifiedDate = ReadProperty(ModifiedDateProperty),
                RowVersion = RowVersion
            };
            return data;
        }
        #endregion

        #region IIdentity Members

        public string AuthenticationType => "Csla";

        public bool IsAuthenticated => UserName != "";

        #endregion

        string IIdentity.Name => UserName;
    }
}
