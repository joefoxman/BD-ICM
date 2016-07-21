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
    public class PartChangeList : ReadOnlyBindingListBase<PartChangeList, PartChange>
    {
        #region [Factory Methods]

        public static PartChangeList GetPartChangeList(int instrumentId, DateTime? startDate, DateTime? endDate)
        {
            return
                DataPortal.Fetch<PartChangeList>(new InstrumentPartChangesCriteria(instrumentId, startDate, endDate));
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(InstrumentPartChangesCriteria criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                var items = repository.FetchChanges(criteria.InstrumentId, criteria.StartDate, criteria.EndDate).ToList();
                AddItems(items);
            }
        }

        private void AddItems(IEnumerable<DataAccess.Dto.PartChange> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(PartChange.GetPartChange(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
