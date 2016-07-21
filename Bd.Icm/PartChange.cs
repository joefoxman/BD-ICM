using System;
using Bd.Icm.DataAccess;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartChange : VersionedObject<PartChange>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<int?> ParentPartIdProperty = RegisterProperty<int?>(c => c.ParentPartId);
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public static readonly PropertyInfo<string> SerialNumberProperty = RegisterProperty<string>(c => c.SerialNumber);
        public static readonly PropertyInfo<string> DocumentNumberProperty = RegisterProperty<string>(c => c.DocumentNumber);
        public static readonly PropertyInfo<int> DashNumberProperty = RegisterProperty<int>(c => c.DashNumber);
        public static readonly PropertyInfo<int> RevisionNumberProperty = RegisterProperty<int>(c => c.RevisionNumber);
        public static readonly PropertyInfo<string> SapPartNumberProperty = RegisterProperty<string>(c => c.SapPartNumber);
        public static readonly PropertyInfo<int?> InstrumentCommitIdProperty = RegisterProperty<int?>(c => c.InstrumentCommitId);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<string> ModifierProperty = RegisterProperty<string>(c => c.Modifier);
        public static readonly PropertyInfo<string> CreatorProperty = RegisterProperty<string>(c => c.Creator);

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

        public int? ParentPartId
        {
            get { return GetProperty(ParentPartIdProperty); }
            set { SetProperty(ParentPartIdProperty, value); }
        }

        public int? InstrumentCommitId
        {
            get { return GetProperty(InstrumentCommitIdProperty); }
            private set { SetProperty(InstrumentCommitIdProperty, value); }
        }

        public string SapPartNumber
        {
            get { return GetProperty(SapPartNumberProperty); }
            set { SetProperty(SapPartNumberProperty, value); }
        }

        public int RevisionNumber
        {
            get { return GetProperty(RevisionNumberProperty); }
            set { SetProperty(RevisionNumberProperty, value); }
        }

        public int DashNumber
        {
            get { return GetProperty(DashNumberProperty); }
            set { SetProperty(DashNumberProperty, value); }
        }

        public string DocumentNumber
        {
            get { return GetProperty(DocumentNumberProperty); }
            set { SetProperty(DocumentNumberProperty, value); }
        }

        public string SerialNumber
        {
            get { return GetProperty(SerialNumberProperty); }
            set { SetProperty(SerialNumberProperty, value); }
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

        public static PartChange NewChildPartChange()
        {
            return DataPortal.CreateChild<PartChange>();
        }

        public static PartChange GetPartChange(DataAccess.Dto.PartChange data)
        {
            return DataPortal.FetchChild<PartChange>(data);
        }

        #endregion

        #region Data Access

        #region Data Access - Fetch

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Dto.PartChange data)
        {
            ImportData(data);
            BusinessRules.CheckRules();
        }

        #endregion //Data Access - Fetch

        #region Data Access - Update

        [UsedImplicitly]
        private void Child_Update()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
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

        private void ImportData(DataAccess.Dto.PartChange data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(ParentPartIdProperty, data.ParentPartId);
            LoadProperty(NameProperty, data.Name);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(DocumentNumberProperty, data.DocumentNumber);
            LoadProperty(SerialNumberProperty, data.SerialNumber);
            LoadProperty(DashNumberProperty, data.DashNumber);
            LoadProperty(SapPartNumberProperty, data.SapPartNumber);
            LoadProperty(InstrumentCommitIdProperty, data.InstrumentCommitId);
            LoadProperty(ModifierProperty, data.Modifier);
            LoadProperty(CreatorProperty, data.Creator);
            LoadProperty(ModificationTypeProperty, data.ModificationType);
            GetConcurrencyData(data.RowVersion);
            LoadAuditFields(data);
            LoadVersions(data);
        }

        internal DataAccess.Dto.PartChange ExportData()
        {
            var data = new DataAccess.Dto.PartChange
            {
                Id = ReadProperty(IdProperty),
                Name = ReadProperty(NameProperty),
                Description = ReadProperty(DescriptionProperty),
                SerialNumber = ReadProperty(SerialNumberProperty),
                SapPartNumber = ReadProperty(SapPartNumberProperty),
                ParentPartId = ReadProperty(ParentPartIdProperty),
                DocumentNumber = ReadProperty(DocumentNumberProperty),
                DashNumber = ReadProperty(DashNumberProperty),
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
