using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IDbVersionRepository : IRepository<DbVersion>
    {
        int GetNextVersion(int modifiedBy);
    }
}
