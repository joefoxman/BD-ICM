using System.Collections.Generic;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        IEnumerable<UserRole> FetchAllByUser(int userId);
    }
}
