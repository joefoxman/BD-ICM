using System;
using System.Linq;
using Bd.Icm.Core;
using Bd.Icm.Criteria;
using Bd.Icm.DataAccess;
using Bd.Icm.Rules;
using Csla;
using Csla.Rules.CommonRules;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class Part : VersionedObject<Part>,
        ICommittable, IPart
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<int?> ParentPartIdProperty = RegisterProperty<int?>(c => c.ParentPartId);
        public static readonly PropertyInfo<PartType> SapPartTypeProperty = RegisterProperty<PartType>(c => c.SapPartType, "SAP Part Type");
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public static readonly PropertyInfo<string> SerialNumberProperty = RegisterProperty<string>(c => c.SerialNumber, "Serial Number");
        public static readonly PropertyInfo<string> DocumentNumberProperty = RegisterProperty<string>(c => c.DocumentNumber, "Document Number");
        public static readonly PropertyInfo<int?> DashNumberProperty = RegisterProperty<int?>(c => c.DashNumber, "Dash Number");
        public static readonly PropertyInfo<int> RevisionNumberProperty = RegisterProperty<int>(c => c.RevisionNumber, "Revision");
        public static readonly PropertyInfo<string> SapPartNumberProperty = RegisterProperty<string>(c => c.SapPartNumber, "SAP Part Number");
        public static readonly PropertyInfo<string> LotCodeProperty = RegisterProperty<string>(c => c.LotCode, "Lot Code");
        public static readonly PropertyInfo<string> DateCodeProperty = RegisterProperty<string>(c => c.DateCode, "Date Code");
        public static readonly PropertyInfo<int?> InstrumentCommitIdProperty = RegisterProperty<int?>(c => c.InstrumentCommitId);
        public static readonly PropertyInfo<Parts> PartsProperty = RegisterProperty<Parts>(c => c.Parts);
        public static readonly PropertyInfo<PartActions> ActionsProperty = RegisterProperty<PartActions>(c => c.Actions);
        public static readonly PropertyInfo<PartMetadatas> MetadataProperty = RegisterProperty<PartMetadatas>(c => c.Metadata);
        public static readonly PropertyInfo<int?> InstrumentIdProperty = RegisterProperty<int?>(c => c.InstrumentId);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<string> MfgPartNumberProperty = RegisterProperty<string>(c => c.MfgPartNumber, "Manufacturer's Part Number");
        public static readonly PropertyInfo<string> ModifierProperty = RegisterProperty<string>(c => c.Modifier);
        public static readonly PropertyInfo<string> CreatorProperty = RegisterProperty<string>(c => c.Creator);
        public static readonly PropertyInfo<string> ManufacturerProperty = RegisterProperty<string>(c => c.Manufacturer);

        public string Manufacturer
        {
            get { return GetProperty(ManufacturerProperty); }
            set { SetProperty(ManufacturerProperty, value); }
        }

        public string Creator
        {
            get { return GetProperty(CreatorProperty); }
        }

        public string Modifier
        {
            get { return GetProperty(ModifierProperty); }
            internal set { SetProperty(ModifierProperty, value);}
        }

        public string MfgPartNumber
        {
            get { return GetProperty(MfgPartNumberProperty); }
            set { SetProperty(MfgPartNumberProperty, value); }
        }

        private int _version;

        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public int? InstrumentId
        {
            get { return GetProperty(InstrumentIdProperty); }
            set { SetProperty(InstrumentIdProperty, value); }
        }

        public Parts Parts => LazyGetProperty(PartsProperty, GetSubParts);
        public PartActions Actions => LazyGetProperty(ActionsProperty, PartActions.NewPartActions);
        public PartMetadatas Metadata => LazyGetProperty(MetadataProperty, PartMetadatas.NewPartMetadatas);

        private Parts GetSubParts()
        {
            return Id == 0 ? Parts.NewParts() : Parts.GetSubParts(Id, _version, true);
        }

        public int? ParentPartId
        {
            get { return GetProperty(ParentPartIdProperty); }
            set { SetProperty(ParentPartIdProperty, value); }
        }

        public string DateCode
        {
            get { return GetProperty(DateCodeProperty); }
            set { SetProperty(DateCodeProperty, value); }
        }

        public int? InstrumentCommitId
        {
            get { return GetProperty(InstrumentCommitIdProperty); }
            private set { SetProperty(InstrumentCommitIdProperty, value); }
        }

        public string LotCode
        {
            get { return GetProperty(LotCodeProperty); }
            set { SetProperty(LotCodeProperty, value); }
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

        public int? DashNumber
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

        public PartType SapPartType
        {
            get { return GetProperty(SapPartTypeProperty); }
            set { SetProperty(SapPartTypeProperty, value); }
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

        #endregion

        internal Part Copy()
        {
            var newPart = IsChild ? NewChildPart() : NewPart();
            newPart.DashNumber = DashNumber;
            newPart.Description = Description;
            newPart.DocumentNumber = DocumentNumber;
            newPart.InstrumentCommitId = null;
            newPart.MfgPartNumber = MfgPartNumber;
            newPart.Name = Name;
            newPart.RevisionNumber = RevisionNumber;
            newPart.SapPartNumber = SapPartNumber;
            newPart.SapPartType = SapPartType;
            newPart.Manufacturer = Manufacturer;
            newPart.Parts.AddRange(Parts.Copy());
            return newPart;
        }

        #region [ICommitable]

        public bool IsSelfCommittable
        {
            get { return !InstrumentCommitId.HasValue; }
        }

        public bool IsCommittable
        {
            get { return IsSelfCommittable && Parts.IsCommittable; }
        }

        public bool IsSelfCommittableForUser(int userId)
        {
            return IsSelfCommittable && ModifiedBy == userId;
        }

        public bool IsCommittableForUser(int userId)
        {
            return IsSelfCommittableForUser(userId) && Parts.IsCommittableForUser(userId);
        }

        public void Commit(int instrumentCommitId)
        {
            InstrumentCommitId = instrumentCommitId;
            Parts.Commit(instrumentCommitId);
        }

        public void Commit(int userId, int instrumentCommitId)
        {
            if (ModifiedBy == userId)
            {
                InstrumentCommitId = instrumentCommitId;
            }
            Parts.Commit(userId, instrumentCommitId);
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new MaxLength(SapPartTypeProperty, 25));

            BusinessRules.AddRule(new Required(NameProperty));
            BusinessRules.AddRule(new MaxLength(NameProperty, 150));

            BusinessRules.AddRule(new MaxLength(DescriptionProperty, 200));
            BusinessRules.AddRule(new MaxLength(DateCodeProperty, 200));

            BusinessRules.AddRule(new MaxLength(DocumentNumberProperty, 50));

            BusinessRules.AddRule(new MinValue<int>(RevisionNumberProperty, 1));
            BusinessRules.AddRule(new MaxLength(SapPartNumberProperty, 50));
            BusinessRules.AddRule(new MaxLength(MfgPartNumberProperty, 50));
            BusinessRules.AddRule(new MaxLength(LotCodeProperty, 50));

            BusinessRules.AddRule(new MaxLength(ManufacturerProperty, 150));
            BusinessRules.AddRule(new MaxLength(SerialNumberProperty, 100));

            BusinessRules.AddRule(new EnumValid<PartType>(SapPartTypeProperty));

            BusinessRules.AddRule(new ParentIdRequired(ParentPartIdProperty, InstrumentIdProperty));
        }

        #endregion

        #region [Factory Methods]

        public static Part NewPart()
        {
            return DataPortal.Create<Part>();
        }

        public static Part NewChildPart()
        {
            return DataPortal.CreateChild<Part>();
        }

        public static Part GetPart(int id, int version = int.MaxValue)
        {
            return DataPortal.Fetch<Part>(new IdVersionCriteria<Part>(id, version, false));
        }

        public static Part GetDeletedPart(int id)
        {
            return DataPortal.Fetch<Part>(new DeletedPartCriteria(id));
        }

        public static Part GetPart(DataAccess.Database.Part data, int version)
        {
            return DataPortal.FetchChild<Part>(data, version);
        }

        public static void DeletePart(int id)
        {
            DataPortal.Delete<Part>(id);
        }

        #endregion

        #region Data Access

        #region Data Access - Create
        [RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(RevisionNumberProperty, 1);
            BusinessRules.CheckRules();
        }

        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        #endregion //Data Access - Create

        #region Data Access - Fetch

        [UsedImplicitly]
        private void DataPortal_Fetch(IdVersionCriteria<Part> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                var data = repository.SingleOrDefault(x => x.Id == criteria.Id, criteria.Version);
                if (data == null)
                    throw new RecordNotFoundException(GetType().Name, criteria.Id.ToString());
                ImportData(data, criteria.Version);
            }
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void DataPortal_Fetch(DeletedPartCriteria criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                var data = repository.Find(x => x.Id == criteria.Id && x.EffectiveTo != int.MaxValue).OrderByDescending(x => x.EffectiveTo).Take(1).ToList();
                if (!data.Any())
                    throw new RecordNotFoundException("Part", criteria.Id.ToString());
                var part = data.First();
                ImportData(part, part.EffectiveTo);
            }
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.Part data, int version)
        {
            ImportData(data, version);
            BusinessRules.CheckRules();
        }

        #endregion //Data Access - Fetch

        #region Data Access - Insert
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            SetDataStamps();
            InsertCore(ExportData());
        }

        private void InsertCore(DataAccess.Database.Part data)
        {
            if (IsSelfDirty)
            {
                using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
                {
                    repository.Insert(data);
                    ImportRecord(data);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_Insert(Instrument parent)
        {
            SetDataStamps();
            var data = ExportData();
            data.InstrumentId = parent.InstrumentId;
            data.ParentPartId = null;
            InsertCore(data);
        }

        [UsedImplicitly]
        private void Child_Insert(Part parent)
        {
            SetDataStamps();
            var data = ExportData();
            data.InstrumentId = null;
            data.ParentPartId = parent.Id;
            InsertCore(data);
        }

        #endregion //Data Access - Insert

        #region Data Access - Update
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            if (IsSelfDirty)
            {
                SetDataStamps();
                using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
                {
                    var data = ExportData();
                    data = repository.Update(data);
                    ImportRecord(data);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_Update(Instrument parent)
        {
            if (IsSelfDirty)
            {
                SetDataStamps();
                using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
                {
                    var data = ExportData();
                    data.InstrumentId = parent.InstrumentId;
                    data.ParentPartId = null;
                    data = repository.Update(data);
                    ImportRecord(data);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_Update(Part parent)
        {
            if (IsSelfDirty)
            {
                SetDataStamps();
                using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
                {
                    var data = ExportData();
                    data.InstrumentId = null;
                    data.ParentPartId = parent.Id;
                    data = repository.Update(data);
                    ImportRecord(data);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        #endregion //Data Access - Update

        #region Data Access - Delete
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(
              new IdVersionCriteria<Part>(ReadProperty(IdProperty)));
        }

        [Transactional(TransactionalTypes.TransactionScope), UsedImplicitly]
        private void DataPortal_Delete(IdVersionCriteria<Part> criteria)
        {
            Metadata.Clear();
            Actions.Clear();
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                SetDataStamps();
                var obj = repository.Delete(ExportData());
                ImportRecord(obj);
            }
        }

        [UsedImplicitly]
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Part, Guid> criteria)
        {
            Metadata.Clear();
            Actions.Clear();
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                SetDataStamps();
                var obj = repository.Delete(ExportData());
                ImportRecord(obj);
            }
        }
        #endregion //Data Access - Delete

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportRecord(DataAccess.Database.Part data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(ParentPartIdProperty, data.ParentPartId);
            LoadProperty(InstrumentIdProperty, data.InstrumentId);
            LoadProperty(SapPartTypeProperty, data.SapPartType);
            LoadProperty(NameProperty, data.Name);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(DocumentNumberProperty, data.DocumentNumber);
            LoadProperty(SerialNumberProperty, data.SerialNumber);
            LoadProperty(DashNumberProperty, data.DashNumber);
            LoadProperty(RevisionNumberProperty, data.RevisionNumber);
            LoadProperty(SapPartNumberProperty, data.SapPartNumber);
            LoadProperty(ManufacturerProperty, data.Manufacturer);
            LoadProperty(MfgPartNumberProperty, data.MfgPartNumber);
            LoadProperty(LotCodeProperty, data.LotCode);
            LoadProperty(DateCodeProperty, data.DateCode);
            LoadProperty(InstrumentCommitIdProperty, data.InstrumentCommitId);
            if (data.Modifier != null)
                LoadProperty(ModifierProperty, data.Modifier.UserName);
            if (data.Creator != null)
                LoadProperty(CreatorProperty, data.Creator.UserName);
            LoadVersions(data);
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
        }

        private void ImportData(DataAccess.Database.Part data, int version)
        {
            _version = version;
            ImportRecord(data);

            using (var actionRepository = RepositoryFactory.Instance.GetRepository<IPartActionRepository>())
            {
                var actions = actionRepository.FetchAll(data.Id, version);
                LoadProperty(ActionsProperty, PartActions.GetPartActions(actions));
            }

            using (var metadataRepository = RepositoryFactory.Instance.GetRepository<IPartMetadataRepository>())
            {
                var metadata = metadataRepository.FetchAll(data.Id, version);
                LoadProperty(MetadataProperty, PartMetadatas.GetPartMetadatas(metadata));
            }
        }

        internal DataAccess.Database.Part ExportData()
        {
            var data = new DataAccess.Database.Part
            {
                Id = ReadProperty(IdProperty),
                InstrumentId = ReadProperty(InstrumentIdProperty),
                SapPartType = (int)ReadProperty(SapPartTypeProperty),
                Name = ReadProperty(NameProperty),
                Description = ReadProperty(DescriptionProperty),
                SerialNumber = ReadProperty(SerialNumberProperty),
                SapPartNumber = ReadProperty(SapPartNumberProperty),
                ParentPartId = ReadProperty(ParentPartIdProperty),
                DocumentNumber = ReadProperty(DocumentNumberProperty),
                DashNumber = ReadProperty(DashNumberProperty),
                MfgPartNumber = ReadProperty(MfgPartNumberProperty),
                Manufacturer = ReadProperty(ManufacturerProperty),
                RevisionNumber = ReadProperty(RevisionNumberProperty),
                LotCode = ReadProperty(LotCodeProperty),
                DateCode = ReadProperty(DateCodeProperty),
                InstrumentCommitId = ReadProperty(InstrumentCommitIdProperty),
                EffectiveFrom = ReadProperty(EffectiveFromProperty),
                EffectiveTo = ReadProperty(EffectiveToProperty),
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
