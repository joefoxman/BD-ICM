using System;
using Csla;

namespace Bd.Icm.Criteria
{
    [Serializable]
    public class DeletedPartCriteria : CriteriaBase<DeletedPartCriteria>
    {
        public int Id { get; set; }

        public DeletedPartCriteria(int id)
        {
            Id = id;
        }
    }
}
