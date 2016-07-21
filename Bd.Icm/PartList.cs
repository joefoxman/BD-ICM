using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartList : ReadOnlyBindingListBase<PartList, PartInfo>
    {
        #region [Factory Methods]

        public static PartList GetPartList(string searchKey)
        {
            return
                DataPortal.Fetch<PartList>(searchKey);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(string searchKey)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                var items = repository.Fetch(int.MaxValue).Where(x => x.Name.Contains(searchKey)).ToList();
                AddItems(items.Distinct(new NameComparer()));
            }
        }

        private class NameComparer : IEqualityComparer<DataAccess.Database.Part>
        {
            public bool Equals(DataAccess.Database.Part x, DataAccess.Database.Part y)
            {
                return x.Name.Equals(y.Name);
            }

            public int GetHashCode(DataAccess.Database.Part obj)
            {
                return obj.Id;
            }
        }

        private void AddItems(IEnumerable<DataAccess.Database.Part> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(PartInfo.GetPartInfo(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
