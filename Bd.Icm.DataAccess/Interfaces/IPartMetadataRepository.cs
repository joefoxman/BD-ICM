using System.Collections.Generic;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Dto;

namespace Bd.Icm.DataAccess
{
    public interface IPartMetadataRepository : IVersionedRepository<PartMetadata>
    {
        IEnumerable<PartMetadata> FetchAll(int partId, int version);
        IEnumerable<PartMetadataChange> GetUncommittedChanges(int instrumentId);
        PartMetadataChange Commit(PartMetadataChange change);
    }
}
