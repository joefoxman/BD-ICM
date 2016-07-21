using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartActions : BusinessListBase<PartActions, PartAction>
    {
        #region [Factory Methods]

        internal static PartActions NewPartActions()
        {
            return new PartActions();
        }

        internal static PartActions GetPartActions(IEnumerable<DataAccess.Database.PartAction> dtos)
        {
            var children = new PartActions();
            children.Fetch(dtos);
            return children;
        }

        public PartActions()
        {

        }
        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void Fetch(IEnumerable<DataAccess.Database.PartAction> dtos)
        {
            RaiseListChangedEvents = false;

            foreach (var child in dtos)
            {
                Add(PartAction.GetPartAction(child));
            }

            RaiseListChangedEvents = true;
        }

        #endregion

        #region [Dto Mapping]

        internal IList<DataAccess.Database.PartAction> ExportDtos()
        {
            return this.Select(child => child.ExportData()).ToList();
        }

        #endregion
    }
}
