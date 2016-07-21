using Csla;
using System;
using Bd.Icm.Core;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class UserInfo : ReadOnlyBase<UserInfo>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);
        public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(c => c.Password);
        public static readonly PropertyInfo<bool> IsDisabledProperty = RegisterProperty<bool>(c => c.IsDisabled);
        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);
        public static readonly PropertyInfo<RoleType> RoleProperty = RegisterProperty<RoleType>(c => c.Role);

        public RoleType Role
        {
            get { return GetProperty(RoleProperty); }
        }

        public string Email
        {
            get { return GetProperty(EmailProperty); }
        }

        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
        }

        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
        }

        public bool IsDisabled
        {
            get { return GetProperty(IsDisabledProperty); }
        }

        public string Password
        {
            get { return GetProperty(PasswordProperty); }
        }

        public string UserName
        {
            get { return GetProperty(UserNameProperty); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        #endregion

        #region [Factory Methods]

        public static UserInfo GetUserInfo(DataAccess.Database.User data)
        {
            return DataPortal.FetchChild<UserInfo>(data);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.User data)
        {
            ImportData(data);
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
            LoadProperty(RoleProperty, data.Role);
        }

        #endregion

    }
}
