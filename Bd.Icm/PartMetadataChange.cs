using System;
using Bd.Icm.DataAccess;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartMetadataChange : VersionedObject<PartMetadataChange>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<int> PartIdProperty = RegisterProperty<int>(c => c.PartId);
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public static readonly PropertyInfo<string> MetaKeyProperty = RegisterProperty<string>(c => c.MetaKey);
        public static readonly PropertyInfo<int?> InstrumentCommitIdProperty = RegisterProperty<int?>(c => c.InstrumentCommitId);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<string> ModifierProperty = RegisterProperty<string>(c => c.Modifier);
        public static readonly PropertyInfo<string> CreatorProperty = RegisterProperty<string>(c => c.Creator);
        public static readonly PropertyInfo<string> MetaValueProperty = RegisterProperty<string>(c => c.MetaValue);

        public string MetaValue
        {
            get { return GetProperty(MetaValueProperty); }
        }

        public string Creator
        {
            get { return GetProperty(CreatorProperty); }
        }

        public string Modifier
        {
            get { return GetProperty(ModifierProperty); }
        }

        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public int PartId
        {
            get { return GetProperty(PartIdProperty); }
            set { SetProperty(PartIdProperty, value); }
        }

        public int? InstrumentCommitId
        {
            get { return GetProperty(InstrumentCommitIdProperty); }
            private set { SetProperty(InstrumentCommitIdProperty, value); }
        }

        public string MetaKey
        {
            get { return GetProperty(MetaKeyProperty); }
            set { SetProperty(MetaKeyProperty, value); }
        }

        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        public void Commit(int commitId)
        {
            InstrumentCommitId = commitId;
        }

        #endregion

        #region [Factory Methods]

        public static PartMetadataChange NewChildPartMetadataChange()
        {
            return DataPortal.CreateChild<PartMetadataChange>();
        }

        public static PartMetadataChange GetPartMetadataChange(DataAccess.Dto.PartMetadataChange data)
        {
            return DataPortal.FetchChild<PartMetadataChange>(data);
        }

        #endregion

        #region Data Access

        #region Data Access - Fetch

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Dto.PartMetadataChange data)
        {
            ImportData(data);
            BusinessRules.CheckRules();
        }

        #endregion //Data Access - Fetch

        #region Data Access - Update

        [UsedImplicitly]
        private void Child_Update()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                data = repository.Commit(data);
                LoadVersions(data);
                LoadAuditFields(data);
            }
            FieldManager.UpdateChildren(this);
        }

        #endregion //Data Access - Update

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Dto.PartMetadataChange data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(PartIdProperty, data.PartId);
            LoadProperty(NameProperty, data.Name);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(MetaKeyProperty, data.MetaKey);
            LoadProperty(MetaValueProperty, data.MetaValue);
            LoadProperty(InstrumentCommitIdProperty, data.InstrumentCommitId);
            LoadProperty(ModifierProperty, data.Modifier);
            LoadProperty(CreatorProperty, data.Creator);
            LoadProperty(ModificationTypeProperty, data.ModificationType);
            GetConcurrencyData(data.RowVersion);
            LoadAuditFields(data);
            LoadVersions(data);
        }

        internal DataAccess.Dto.PartMetadataChange ExportData()
        {
            var data = new DataAccess.Dto.PartMetadataChange
            {
                Id = ReadProperty(IdProperty),
                Name = ReadProperty(NameProperty),
                Description = ReadProperty(DescriptionProperty),
                MetaKey = ReadProperty(MetaKeyProperty),
                MetaValue = ReadProperty(MetaValueProperty),
                PartId = ReadProperty(PartIdProperty),
                InstrumentCommitId = ReadProperty(InstrumentCommitIdProperty),
                CreatedBy = ReadProperty(CreatedByProperty),
                CreatedDate = ReadProperty(CreatedDateProperty),
                ModifiedBy = ReadProperty(ModifiedByProperty),
                ModifiedDate = ReadProperty(ModifiedDateProperty),
                Modifier = ReadProperty(ModifierProperty),
                Creator = ReadProperty(CreatorProperty),
                RowVersion = RowVersion
            };

            return data;
        }

        #endregion

    }
}
