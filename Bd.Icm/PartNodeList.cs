using Csla;
using System;
using System.Collections.Generic;
using Bd.Icm.Criteria;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartNodeList : ReadOnlyBindingListBase<PartNodeList, PartNode>
    {
        #region [Factory Methods]

        public static PartNodeList GetPartNodeList(int? instrumentId, int? parentPartId, int version = int.MaxValue)
        {
            return DataPortal.Fetch<PartNodeList>(new PartParentVersionCriteria<PartNodeList>(parentPartId, instrumentId, version, false));
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(PartParentVersionCriteria<PartNodeList> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                var items = repository.FetchPartHeirarchy(criteria.InstrumentId, criteria.ParentPartId, criteria.Version);
                AddItems(items);
            }
        }

        private void AddItems(IEnumerable<DataAccess.Dto.PartNode> items)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in items)
            {
                Add(PartNode.GetPartNode(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;

        }
        #endregion
    }
}
