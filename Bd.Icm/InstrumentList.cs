using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class InstrumentList : ReadOnlyBindingListBase<InstrumentList, InstrumentInfo>
    {
        #region [Factory Methods]

        public static InstrumentList GetInstrumentList()
        {
            return
                DataPortal.Fetch<InstrumentList>();
        }

        public static InstrumentList GetInstrumentList(string searchKey)
        {
            return
                DataPortal.Fetch<InstrumentList>(searchKey);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                AddItems(repository.FetchAll(int.MaxValue));
            }
        }

        [UsedImplicitly]
        private void DataPortal_Fetch(string searchKey)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                var items = repository.Fetch(int.MaxValue).Where(x => x.SerialNumber.Contains(searchKey));
                AddItems(items);
            }
        }

        private void AddItems(IEnumerable<DataAccess.Database.Instrument> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(InstrumentInfo.GetInstrumentInfo(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
