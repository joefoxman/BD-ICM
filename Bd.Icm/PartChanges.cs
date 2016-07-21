using System;
using System.Collections.Generic;
using Csla;

namespace Bd.Icm
{
    [Serializable]
    public class PartChanges : BusinessListBase<PartChanges, PartChange>
    {
        #region [Factory Methods]

        internal static PartChanges NewPartChanges()
        {
            return new PartChanges();
        }

        internal static PartChanges GetPartChanges(IEnumerable<DataAccess.Dto.PartChange> dtos)
        {
            var children = new PartChanges();
            children.Fetch(dtos);
            return children;
        }

        public PartChanges()
        {

        }
        #endregion

        #region [Data Access]

        private void Fetch(IEnumerable<DataAccess.Dto.PartChange> dtos)
        {
            RaiseListChangedEvents = false;

            foreach (var child in dtos)
            {
                Add(PartChange.GetPartChange(child));
            }

            RaiseListChangedEvents = true;
        }

        #endregion

        #region [Dto Mapping]
        #endregion
    }
}
