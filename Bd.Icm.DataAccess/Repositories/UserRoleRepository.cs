using System.Collections.Generic;
using System.Linq;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public IEnumerable<UserRole> FetchAllByUser(int userId)
        {
            return FetchAll().Where(x => x.UserId == userId);
        }
    }
}
