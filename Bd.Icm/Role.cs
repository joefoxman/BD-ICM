using System.Globalization;
using Csla;
using Csla.Rules.CommonRules;
using System;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class Role : DataObject<Role>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<bool> IsDisabledProperty = RegisterProperty<bool>(c => c.IsDisabled);

        public bool IsDisabled
        {
            get { return GetProperty(IsDisabledProperty); }
            set { SetProperty(IsDisabledProperty, value); }
        }

        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new Required(DescriptionProperty));
            BusinessRules.AddRule(new MaxLength(DescriptionProperty, 50));
        }

        #endregion

        #region [Factory Methods]

        public static Role NewRole()
        {
            return DataPortal.Create<Role>();
        }

        public static Role GetRole(int id)
        {
            return DataPortal.Fetch<Role>(id);
        }

        public static Role GetRole(string username)
        {
            return DataPortal.Fetch<Role>(username);
        }

        public static void DeleteRole(int id)
        {
            DataPortal.Delete<Role>(id);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void DataPortal_Fetch(int id)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IRoleRepository>())
            {
                var data = repository.SingleOrDefault(x => x.Id == id);
                if (data == null)
                    throw new RecordNotFoundException("Role", id.ToString(CultureInfo.InvariantCulture), "Record not found.");

                ImportData(data);
            }
            BusinessRules.CheckRules();
        }

        protected override void DataPortal_Insert()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IRoleRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                repository.Insert(data);
                LoadProperty(IdProperty, data.Id);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
            }

            FieldManager.UpdateChildren(this);
        }

        protected override void DataPortal_Update()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IRoleRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                repository.Update(data);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
            }
            FieldManager.UpdateChildren(this);
        }

        protected override void DataPortal_DeleteSelf()
        {
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IRoleRepository>())
            {
                repository.Delete(ExportData());
            }
        }

        [UsedImplicitly]
        private void DataPortal_Delete(SingleCriteria<Role, int> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IRoleRepository>())
            {
                var data = repository.First(x => x.Id == criteria.Value);
                repository.Delete(data);
            }
        }

        #endregion

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.Role data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(IsDisabledProperty, data.IsDisabled);
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
        }

        private DataAccess.Database.Role ExportData()
        {
            SetDataStamps();

            var data = new DataAccess.Database.Role
            {
                Id = ReadProperty(IdProperty),
                Description = ReadProperty(DescriptionProperty),
                IsDisabled = ReadProperty(IsDisabledProperty),
                CreatedBy = ReadProperty(CreatedByProperty),
                CreatedDate = ReadProperty(CreatedDateProperty),
                ModifiedBy = ReadProperty(ModifiedByProperty),
                ModifiedDate = ReadProperty(ModifiedDateProperty),
                RowVersion = RowVersion
            };
            return data;
        }
        #endregion

    }
}
