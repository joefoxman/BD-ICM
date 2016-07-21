using Csla;
using System;
using Bd.Icm.DataAccess;
using Csla.Rules.CommonRules;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartMetadata : VersionedObject<PartMetadata> 
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<string> MetaKeyProperty = RegisterProperty<string>(c => c.MetaKey, "Parameter");
        public static readonly PropertyInfo<string> MetaValueProperty = RegisterProperty<string>(c => c.MetaValue, "Value");
        public static readonly PropertyInfo<UserInfo> ModifierProperty = RegisterProperty<UserInfo>(c => c.Modifier);

        public UserInfo Modifier => GetProperty(ModifierProperty);

        public string MetaValue
        {
            get { return GetProperty(MetaValueProperty); }
            set { SetProperty(MetaValueProperty, value); }
        }

        public string MetaKey
        {
            get { return GetProperty(MetaKeyProperty); }
            set { SetProperty(MetaKeyProperty, value); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new Required(MetaKeyProperty));
            BusinessRules.AddRule(new MaxLength(MetaKeyProperty, 50));
            BusinessRules.AddRule(new Required(MetaValueProperty));
            BusinessRules.AddRule(new MaxLength(MetaValueProperty, 50));
        }

        #endregion

        #region [Factory Methods]

        public static PartMetadata NewPartMetadata()
        {
            return DataPortal.CreateChild<PartMetadata>();
        }

        public static PartMetadata GetPartMetadata(DataAccess.Database.PartMetadata data)
        {
            return DataPortal.FetchChild<PartMetadata>(data);
        }

        public static void DeletePartMetadata(int id)
        {
            DataPortal.Delete<PartMetadata>(new SingleCriteria<PartMetadata, int>(id));
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.PartMetadata data)
        {
            ImportData(data);
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void Child_Insert(Part parent)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataRepository>())
            {
                SetDataStamps();
                var data = ExportData(parent);
                repository.Insert(data);
                LoadProperty(IdProperty, data.Id);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
            }

            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_Update(Part parent)
        {
            if (IsSelfDirty)
            {
                using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataRepository>())
                {
                    SetDataStamps();
                    var data = ExportData(parent);
                    repository.Update(data);
                    GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_DeleteSelf(Part parent)
        {
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataRepository>())
            {
                repository.Delete(ExportData(parent));
            }
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.PartMetadata data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(MetaValueProperty, data.MetaValue);
            LoadProperty(MetaKeyProperty, data.MetaKey);
            LoadProperty(ModifierProperty, UserInfo.GetUserInfo(data.Modifier));
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
        }

        internal DataAccess.Database.PartMetadata ExportData()
        {
            SetDataStamps();

            var data = new DataAccess.Database.PartMetadata
            {
                Id = ReadProperty(IdProperty),
                MetaValue = ReadProperty(MetaValueProperty),
                MetaKey = ReadProperty(MetaKeyProperty),
                CreatedBy = ReadProperty(CreatedByProperty),
                CreatedDate = ReadProperty(CreatedDateProperty),
                ModifiedBy = ReadProperty(ModifiedByProperty),
                ModifiedDate = ReadProperty(ModifiedDateProperty),
                RowVersion = RowVersion
            };
            return data;
        }

        internal DataAccess.Database.PartMetadata ExportData(Part parent)
        {
            var data = ExportData();
            data.PartId = parent.Id;
            return data;
        }
        #endregion

    }
}
