using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IPartMetadataVersionRepository : IRepository<PartMetadataVersion>
    {
        int GetNewId(int createdBy);
    }
}
