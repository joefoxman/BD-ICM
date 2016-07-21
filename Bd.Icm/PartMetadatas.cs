using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartMetadatas : BusinessListBase<PartMetadatas, PartMetadata>
    {
        #region [Factory Methods]

        internal static PartMetadatas NewPartMetadatas()
        {
            return new PartMetadatas();
        }

        internal static PartMetadatas GetPartMetadatas(IEnumerable<DataAccess.Database.PartMetadata> dtos)
        {
            var children = new PartMetadatas();
            children.Fetch(dtos);
            return children;
        }

        public PartMetadatas()
        {

        }
        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void Fetch(IEnumerable<DataAccess.Database.PartMetadata> dtos)
        {
            RaiseListChangedEvents = false;

            foreach (var child in dtos)
            {
                Add(PartMetadata.GetPartMetadata(child));
            }

            RaiseListChangedEvents = true;
        }

        #endregion

        #region [Dto Mapping]

        internal IList<DataAccess.Database.PartMetadata> ExportDtos()
        {
            return this.Select(child => child.ExportData()).ToList();
        }

        #endregion
    }
}
