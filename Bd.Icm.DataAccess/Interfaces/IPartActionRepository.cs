using System.Collections.Generic;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IPartActionRepository : IVersionedRepository<PartAction>
    {
        IEnumerable<PartAction> FetchAll(int partId, int version);
    }
}
