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
    public class Instrument : VersionedObject<Instrument>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> InstrumentIdProperty = RegisterProperty<int>(c => c.InstrumentId);
        public static readonly PropertyInfo<InstrumentType> SapPartTypeProperty = RegisterProperty<InstrumentType>(c => c.SapPartType);
        public static readonly PropertyInfo<string> NickNameProperty = RegisterProperty<string>(c => c.NickName);
        public static readonly PropertyInfo<string> SerialNumberProperty = RegisterProperty<string>(c => c.SerialNumber);
        public static readonly PropertyInfo<Parts> PartsProperty = RegisterProperty<Parts>(c => c.Parts);
        public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
        public static readonly PropertyInfo<int> MajorRevisionProperty = RegisterProperty<int>(c => c.MajorRevision);
        public static readonly PropertyInfo<int> MinorRevisionProperty = RegisterProperty<int>(c => c.MinorRevision);

        public int MinorRevision
        {
            get { return GetProperty(MinorRevisionProperty); }
            set { SetProperty(MinorRevisionProperty, value); }
        }

        public int MajorRevision
        {
            get { return GetProperty(MajorRevisionProperty); }
            internal set { SetProperty(MajorRevisionProperty, value); }
        }

        private int _version;

        public string Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }

        public Parts Parts => LazyGetProperty(PartsProperty, GetParentParts);

        private Parts GetParentParts()
        {
            return InstrumentId == 0 ? Parts.NewParts() : Parts.GetInstrumentParts(InstrumentId, _version, true);
        }

        public string SerialNumber
        {
            get { return GetProperty(SerialNumberProperty); }
            set { SetProperty(SerialNumberProperty, value); }
        }

        public InstrumentType SapPartType
        {
            get { return GetProperty(SapPartTypeProperty); }
            set { SetProperty(SapPartTypeProperty, value); }
        }

        public string NickName
        {
            get { return GetProperty(NickNameProperty); }
            set { SetProperty(NickNameProperty, value); }
        }

        public int InstrumentId
        {
            get { return GetProperty(InstrumentIdProperty); }
            internal set {  SetProperty(InstrumentIdProperty, value);}
        }

        #endregion

        #region [Methods]

        public Instrument Copy()
        {
            var newInstrument = NewInstrument();
            newInstrument.NickName = NickName;
            newInstrument.SapPartType = SapPartType;
            newInstrument.Type = Type;
            newInstrument.MajorRevision = 0;
            newInstrument.MinorRevision = 0;
            newInstrument.Parts.AddRange(Parts.Copy());
            return newInstrument;
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new Required(TypeProperty));
            BusinessRules.AddRule(new MaxLength(TypeProperty, 200));

            BusinessRules.AddRule(new Required(NickNameProperty));
            BusinessRules.AddRule(new MaxLength(NickNameProperty, 150));

            BusinessRules.AddRule(new Required(SerialNumberProperty));
            BusinessRules.AddRule(new MaxLength(SerialNumberProperty, 100));

            BusinessRules.AddRule(new MinValue<int>(MajorRevisionProperty, 0));
            BusinessRules.AddRule(new MinValue<int>(MinorRevisionProperty, 0));

            BusinessRules.AddRule(new EnumRequired<InstrumentType>(SapPartTypeProperty, InstrumentType.None));
            BusinessRules.AddRule(new EnumValid<InstrumentType>(SapPartTypeProperty));
        }

        #endregion

        #region [Factory Methods]

        public static Instrument NewInstrument()
        {
            return DataPortal.Create<Instrument>();
        }

        public static Instrument GetInstrument(int id, bool topLevelOnly = true, int version = int.MaxValue)
        {
            return DataPortal.Fetch<Instrument>(new IdVersionCriteria<Instrument>(id, version, topLevelOnly));
        }

        public static void DeleteInstrument(int id)
        {
            DataPortal.Delete<Instrument>(id);
        }

        #endregion

        #region Data Access

        #region Data Access - Create
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }
        #endregion //Data Access - Create

        #region Data Access - Fetch

        [UsedImplicitly]
        private void DataPortal_Fetch(IdVersionCriteria<Instrument> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                var data = repository.SingleOrDefault(x => x.Id == criteria.Id, criteria.Version);
                if (data == null)
                    throw new RecordNotFoundException(GetType().Name, criteria.Id.ToString());
                ImportData(data, criteria.Version, criteria.TopLevelOnly);
            }
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.Instrument data)
        {
            ImportData(data, data.EffectiveTo, false);
            BusinessRules.CheckRules();
        }

        #endregion //Data Access - Fetch

        #region Data Access - Insert
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                repository.Insert(data);
                LoadProperty(InstrumentIdProperty, data.Id);
                LoadVersions(data);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
            }

            FieldManager.UpdateChildren(this);
        }
        #endregion //Data Access - Insert

        #region Data Access - Update
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            if (IsSelfDirty)
            {
                using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
                {
                    SetDataStamps();
                    var data = ExportData();
                    data = repository.Update(data);
                    LoadVersions(data);
                    GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
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
              new IdVersionCriteria<Instrument>(ReadProperty(InstrumentIdProperty)));
        }

        [Transactional(TransactionalTypes.TransactionScope), UsedImplicitly]
        private void DataPortal_Delete(IdVersionCriteria<Instrument> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                repository.Delete(ExportData());
            }
        }

        [UsedImplicitly]
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Instrument, Guid> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                repository.Delete(ExportData());
            }
        }
        #endregion //Data Access - Delete

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.Instrument data, int version, bool topLevelOnly)
        {
            _version = version;
            LoadProperty(TypeProperty, data.Type);
            LoadProperty(InstrumentIdProperty, data.Id);
            LoadProperty(SapPartTypeProperty, data.SapPartType);
            LoadProperty(NickNameProperty, data.NickName);
            LoadProperty(SerialNumberProperty, data.SerialNumber);
            LoadProperty(MajorRevisionProperty, data.MajorRevision);
            LoadProperty(MinorRevisionProperty, data.MinorRevision);
            LoadProperty(PartsProperty,
                topLevelOnly
                    ? Parts.GetInstrumentParts(data.Id, version, true)
                    : Parts.GetParts(data.InstrumentVersion.Parts.Where(x => IsInVersion(x, version)), version));
            LoadVersions(data);
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
        }

        internal DataAccess.Database.Instrument ExportData()
        {
            var data = new DataAccess.Database.Instrument
            {
                Id = ReadProperty(InstrumentIdProperty),
                Type = ReadProperty(TypeProperty),
                SapPartType = (int)ReadProperty(SapPartTypeProperty),
                NickName = ReadProperty(NickNameProperty),
                SerialNumber = ReadProperty(SerialNumberProperty),
                MajorRevision = ReadProperty(MajorRevisionProperty),
                MinorRevision = ReadProperty(MinorRevisionProperty),
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
