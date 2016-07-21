using System;
using Csla;

namespace Bd.Icm.Criteria
{
    [Serializable]
    public class VersionedCriteria<T> : CriteriaBase<VersionedCriteria<T>>
    {
        private readonly int _version;

        public int Version => _version;

        public VersionedCriteria(int version)
        {
            _version = version;
        }

        public VersionedCriteria()
        {
            _version = int.MaxValue;
        }
    }
}
