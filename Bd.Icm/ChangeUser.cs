using System;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class ChangeUser : ReadOnlyBase<ChangeUser>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);
        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public static readonly PropertyInfo<int> ChangeCountProperty = RegisterProperty<int>(c => c.ChangeCount);

        public int ChangeCount
        {
            get { return GetProperty(ChangeCountProperty); }
        }

        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
        }

        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
        }

        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
        }

        #endregion

        #region [Factory Methods]

        public static ChangeUser GetChangeUser(DataAccess.Dto.ChangeUser data)
        {
            return DataPortal.FetchChild<ChangeUser>(data);
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Dto.ChangeUser data)
        {
            ImportData(data);
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Dto.ChangeUser data)
        {
            LoadProperty(UserIdProperty, data.UserId);
            LoadProperty(FirstNameProperty, data.FirstName);
            LoadProperty(LastNameProperty, data.LastName);
            LoadProperty(ChangeCountProperty, data.ChangeCount);
        }

        #endregion

    }
}
