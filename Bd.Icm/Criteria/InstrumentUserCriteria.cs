using System;
using Csla;

namespace Bd.Icm.Criteria
{
    [Serializable]
    public class InstrumentUserCriteria : CriteriaBase<InstrumentUserCriteria>
    {
        private readonly int _instrumentId;
        private readonly int _userId;

        public int InstrumentId => _instrumentId;
        public int UserId => _userId;

        public InstrumentUserCriteria(int instrumentId, int userId)
        {
            _instrumentId = instrumentId;
            _userId = userId;
        }
    }
}
