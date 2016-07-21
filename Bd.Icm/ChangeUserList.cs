using Csla;
using System;
using System.Collections.Generic;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class ChangeUserList : ReadOnlyBindingListBase<ChangeUserList, ChangeUser>
    {
        #region [Factory Methods]

        public static ChangeUserList GetChangeUserList(int instrumentId)
        {
            return
                DataPortal.Fetch<ChangeUserList>(instrumentId);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(int instrumentId)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                var items = repository.GetUsersWithUncommitedChanges(instrumentId);
                AddItems(items);
            }
        }

        private void AddItems(IEnumerable<DataAccess.Dto.ChangeUser> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(ChangeUser.GetChangeUser(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
