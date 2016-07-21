using Csla;
using System;
using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Bd.Icm.Rules;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class UserRole : DataObject<UserRole> 
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);
        public static readonly PropertyInfo<RoleType> RoleProperty = RegisterProperty<RoleType>(c => c.Role);

        public RoleType Role
        {
            get { return GetProperty(RoleProperty); }
            set { SetProperty(RoleProperty, value); }
        }

        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
            set { SetProperty(UserIdProperty, value); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new EnumRequired<RoleType>(RoleProperty, RoleType.Unknown));
        }

        #endregion

        #region [Factory Methods]

        public static UserRole NewUserRole()
        {
            return DataPortal.CreateChild<UserRole>();
        }

        public static UserRole GetUserRole(DataAccess.Database.UserRole data)
        {
            return DataPortal.FetchChild<UserRole>(data);
        }

        public static void DeleteUserRole(int id)
        {
            DataPortal.Delete<UserRole>(new SingleCriteria<UserRole, int>(id));
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.UserRole data)
        {
            ImportData(data);
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void Child_Insert(User parent)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRoleRepository>())
            {
                SetDataStamps();
                var data = ExportData(parent);
                repository.Insert(data);
                LoadProperty(IdProperty, data.Id);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
            }

            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_Update(User parent)
        {
            if (IsSelfDirty)
            {
                using (var repository = RepositoryFactory.Instance.GetRepository<IUserRoleRepository>())
                {
                    SetDataStamps();
                    var data = ExportData(parent);
                    repository.Update(data);
                    GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_DeleteSelf(User parent)
        {
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IUserRoleRepository>())
            {
                repository.Delete(ExportData(parent));
            }
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.UserRole data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(UserIdProperty, data.UserId);
            LoadProperty(RoleProperty, data.RoleId);
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
        }

        internal DataAccess.Database.UserRole ExportData()
        {
            SetDataStamps();

            var data = new DataAccess.Database.UserRole
            {
                Id = ReadProperty(IdProperty),
                RoleId = (int)ReadProperty(RoleProperty),
                UserId = ReadProperty(UserIdProperty),
                CreatedBy = ReadProperty(CreatedByProperty),
                CreatedDate = ReadProperty(CreatedDateProperty),
                ModifiedBy = ReadProperty(ModifiedByProperty),
                ModifiedDate = ReadProperty(ModifiedDateProperty),
                RowVersion = RowVersion
            };
            return data;
        }

        internal DataAccess.Database.UserRole ExportData(User parent)
        {
            var data = ExportData();
            data.UserId = parent.Id;
            return data;
        }
        #endregion

    }
}
