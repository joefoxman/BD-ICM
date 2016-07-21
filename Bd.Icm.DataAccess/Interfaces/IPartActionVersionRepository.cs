using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IPartActionVersionRepository : IRepository<PartActionVersion>
    {
        int GetNewId(int createdBy);
    }
}
