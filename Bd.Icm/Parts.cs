using System;
using System.Collections.Generic;
using System.Linq;
using Bd.Icm.Criteria;
using Bd.Icm.DataAccess;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class Parts : BusinessListBase<Parts, Part>,
        ICommittable
    {
        #region [ICommitable]

        public bool IsCommittable
        {
            get { return this.Any(x => x.IsCommittable); }
        }

        public bool IsSelfCommittable
        {
            get { return IsCommittable; }
        }

        public bool IsCommittableForUser(int userId)
        {
            return this.Any(x => x.IsCommittableForUser(userId));
        }

        public bool IsSelfCommittableForUser(int userId)
        {
            return IsCommittableForUser(userId);
        }

        public void Commit(int instrumentCommitId)
        {
            foreach (var item in this)
            {
                item.Commit(instrumentCommitId);
            }
        }

        public void Commit(int userId, int instrumentCommitId)
        {
            foreach (var item in this)
            {
                item.Commit(userId, instrumentCommitId);
            }
        }

        #endregion

        internal IEnumerable<Part> Copy()
        {
            foreach (var part in this)
            {
                yield return part.Copy();
            }
        }

        #region [Factory Methods]

        internal static Parts NewParts()
        {
            return new Parts();
        }

        internal static Parts GetParts(IEnumerable<DataAccess.Database.Part> dtos, int version)
        {
            var children = new Parts();
            children.Fetch(dtos, version, false);
            return children;
        }

        internal static Parts GetInstrumentParts(int instrumentId, int version, bool topLevelOnly)
        {
            return DataPortal.Fetch<Parts>(new PartParentVersionCriteria<Parts>(null, instrumentId, version, topLevelOnly));
        }

        internal static Parts GetSubParts(int parentPartId, int version, bool topLevelOnly)
        {
            return DataPortal.Fetch<Parts>(new PartParentVersionCriteria<Parts>(parentPartId, null, version, topLevelOnly));
        }

        public Parts()
        {

        }
        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(PartParentVersionCriteria<Parts> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                var parts = criteria.ParentPartId.HasValue ? 
                    repository.FetchAllByParent(criteria.ParentPartId.Value, criteria.Version) : 
                    repository.FetchAllByInstrument(criteria.InstrumentId.Value, criteria.Version);
                Fetch(parts, criteria.Version, criteria.TopLevelOnly);
            }
        }

        private void Fetch(IEnumerable<DataAccess.Database.Part> dtos, int version, bool topLevelOnly) {
            RaiseListChangedEvents = false;

            if (topLevelOnly) {
                if (dtos.Any()) {
                    Add(Part.GetPart(dtos.ElementAt(0), version));
                }
            }
            else {
                foreach (var child in dtos) {
                    Add(Part.GetPart(child, version));
                }
            }
            RaiseListChangedEvents = true;
        }

        #endregion

        #region [Dto Mapping]

        internal IList<DataAccess.Database.Part> ExportDtos()
        {
            return this.Select(child => child.ExportData()).ToList();
        }

        #endregion
    }
}
