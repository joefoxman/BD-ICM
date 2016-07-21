using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class UserRoles : BusinessListBase<UserRoles, UserRole>
    {
        #region [Factory Methods]

        internal static UserRoles NewUserRoles()
        {
            return new UserRoles();
        }

        internal static UserRoles GetUserRoles(IEnumerable<DataAccess.Database.UserRole> dtos)
        {
            var children = new UserRoles();
            children.Fetch(dtos);
            return children;
        }

        public UserRoles()
        {

        }
        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void Fetch(IEnumerable<DataAccess.Database.UserRole> dtos)
        {
            RaiseListChangedEvents = false;

            foreach (var child in dtos)
            {
                Add(UserRole.GetUserRole(child));
            }

            RaiseListChangedEvents = true;
        }

        #endregion

        #region [Dto Mapping]

        internal IList<DataAccess.Database.UserRole> ExportDtos()
        {
            return this.Select(child => child.ExportData()).ToList();
        }

        #endregion
    }
}
