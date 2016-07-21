using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class UserList : ReadOnlyBindingListBase<UserList, UserInfo>
    {
        #region [Factory Methods]

        public static UserList GetUserList(bool includeDisabled = false)
        {
            return
                DataPortal.Fetch<UserList>(includeDisabled);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(bool includeDisabled)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRepository>())
            {
                AddItems(repository.FetchAll().Where(x => includeDisabled || !x.IsDisabled));
            }
        }

        private void AddItems(IEnumerable<DataAccess.Database.User> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(UserInfo.GetUserInfo(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
