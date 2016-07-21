using System;

namespace Bd.Icm.Criteria
{
    [Serializable]
    public class PartParentVersionCriteria<T> : VersionedCriteria<PartParentVersionCriteria<T>>
    {
        private readonly int? _instrumentId;
        private readonly int? _parentPartId;
        private readonly bool _topLevelOnly;

        public int? ParentPartId => _parentPartId;
        public int? InstrumentId => _instrumentId;
        public bool TopLevelOnly => _topLevelOnly;

        public PartParentVersionCriteria(int? parentPartId, int? instrumentId, int version, bool topLevelOnly)
            : base(version)
        {
            _parentPartId = parentPartId;
            _instrumentId = instrumentId;
            _topLevelOnly = topLevelOnly;
        }
    }
}
