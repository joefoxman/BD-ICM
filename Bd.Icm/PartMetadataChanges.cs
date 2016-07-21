using System;
using System.Collections.Generic;
using Csla;

namespace Bd.Icm
{
    [Serializable]
    public class PartMetadataChanges : BusinessListBase<PartMetadataChanges, PartMetadataChange>
    {
        #region [Factory Methods]

        internal static PartMetadataChanges NewPartMetadataChanges()
        {
            return new PartMetadataChanges();
        }

        internal static PartMetadataChanges GetPartMetadataChanges(IEnumerable<DataAccess.Dto.PartMetadataChange> dtos)
        {
            var children = new PartMetadataChanges();
            children.Fetch(dtos);
            return children;
        }

        public PartMetadataChanges()
        {

        }
        #endregion

        #region [Data Access]

        private void Fetch(IEnumerable<DataAccess.Dto.PartMetadataChange> dtos)
        {
            RaiseListChangedEvents = false;

            foreach (var child in dtos)
            {
                Add(PartMetadataChange.GetPartMetadataChange(child));
            }

            RaiseListChangedEvents = true;
        }

        #endregion

        #region [Dto Mapping]
        #endregion
    }
}
