using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IPartVersionRepository : IRepository<PartVersion>
    {
        int GetNewId(int createdBy);
    }
}
