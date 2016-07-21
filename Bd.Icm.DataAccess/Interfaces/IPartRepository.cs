using System.Collections.Generic;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Dto;

namespace Bd.Icm.DataAccess
{
    public interface IPartRepository : IVersionedRepository<Part>
    {
        IEnumerable<Part> FetchAllByParent(int parentId, int version);
        IEnumerable<Part> FetchAllByInstrument(int instrumentId, int version);
        IEnumerable<PartNode> FetchPartHeirarchy(int? instrumentId, int? parentPartId, int version);
        IEnumerable<PartChange> GetUncommittedChanges(int instrumentId);
        PartChange Commit(PartChange change);
    }
}
