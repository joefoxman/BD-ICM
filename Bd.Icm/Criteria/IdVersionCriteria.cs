using System;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Bd.Icm.Criteria
{
    [Serializable]
    public class IdVersionCriteria<T> : VersionedCriteria<IdVersionCriteria<T>>
    {
        private readonly int _id;
        private readonly bool _topLevelOnly;

        public int Id => _id;
        public bool TopLevelOnly => _topLevelOnly;

        public IdVersionCriteria(int id, int version, bool topLevelOnly)
            : base(version)
        {
            _id = id;
            _topLevelOnly = topLevelOnly;
        }

        public IdVersionCriteria(int id)
        {
            _id = id;
        }
    }
}
