using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using Bd.Icm.Criteria;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartSearch : ReadOnlyBindingListBase<PartSearch, PartSearchResult>
    {
        #region [Factory Methods]

        public static PartSearch GetPartSearch(int instrumentId, string searchKey, int version = int.MaxValue)
        {
            return
                DataPortal.Fetch<PartSearch>(new InstrumentPartSearchCriteria(instrumentId, version, searchKey));
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(InstrumentPartSearchCriteria criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                var items = repository.SearchInstrumentParts(criteria.InstrumentId, criteria.SearchKey, criteria.Version);
                AddItems(items.ToList());
            }
        }

        private void AddItems(IEnumerable<DataAccess.Dto.PartSearchResult> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(PartSearchResult.GetPartSearchResult(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
