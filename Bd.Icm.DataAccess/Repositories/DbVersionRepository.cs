using System.Data;
using System.Linq;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class DbVersionRepository : Repository<DbVersion>, IDbVersionRepository
    {
        public int GetNextVersion(int modifiedBy)
        {
            using (var entities = new BdIcmEntities())
            {
                var result = entities.spGetNextDbVersion(modifiedBy).ToArray();
                if (!result.Any())
                {
                    throw new DataException("No results returned from DbVersion.GetNextVersion.");
                }
                var value = result.First();
                if ((value == null))
                {
                    throw new DataException("Empty result returned from DbVersion.GetNextVersion.");
                }
                return value.Value;
            }
        }
    }
}
