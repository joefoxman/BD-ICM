using Csla;
using Csla.Rules.CommonRules;
using System;
using Bd.Icm.DataAccess;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class InstrumentCommit : DataObject<InstrumentCommit>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(c => c.Notes);
        public static readonly PropertyInfo<int> InstrumentIdProperty = RegisterProperty<int>(c => c.InstrumentId);
        public static readonly PropertyInfo<int> RevisionProperty = RegisterProperty<int>(c => c.Revision);
        public static readonly PropertyInfo<int> EffectiveToProperty = RegisterProperty<int>(c => c.EffectiveTo);

        public int EffectiveTo
        {
            get { return GetProperty(EffectiveToProperty); }
            set {  SetProperty(EffectiveToProperty, value);}
        }

        public int Revision
        {
            get { return GetProperty(RevisionProperty); }
            set { SetProperty(RevisionProperty, value); }
        }

        public int InstrumentId
        {
            get { return GetProperty(InstrumentIdProperty); }
            set { SetProperty(InstrumentIdProperty, value); }
        }

        public int Id => GetProperty(IdProperty);

        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new MaxLength(NotesProperty, 255));
            BusinessRules.AddRule(new MinValue<int>(InstrumentIdProperty, 1));
            BusinessRules.AddRule(new MinValue<int>(RevisionProperty, 0));
        }

        #endregion

        #region [Factory Methods]

        public static InstrumentCommit NewInstrumentCommit()
        {
            return DataPortal.Create<InstrumentCommit>();
        }

        public static InstrumentCommit GetInstrumentCommit(int id)
        {
            return DataPortal.Fetch<InstrumentCommit>(id);
        }

        public static void DeleteInstrumentCommit(int id)
        {
            DataPortal.Delete<InstrumentCommit>(new SingleCriteria<InstrumentCommit, int>(id));
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

        [UsedImplicitly]
        private void DataPortal_Fetch(int id)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentCommitRepository>())
            {
                var data = repository.SingleOrDefault(x => x.Id == id);
                if (data == null)
                    throw new RecordNotFoundException(GetType().Name, id.ToString());
                ImportData(data);
            }
            BusinessRules.CheckRules();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentCommitRepository>())
            {
                SetDataStamps();
                var data = ExportData();
                repository.Insert(data);
                LoadProperty(IdProperty, data.Id);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
            }

            FieldManager.UpdateChildren(this);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            if (IsSelfDirty)
            {
                using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentCommitRepository>())
                {
                    SetDataStamps();
                    var data = ExportData();
                    repository.Update(data);
                    GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentCommitRepository>())
            {
                repository.Delete(ExportData());
            }
        }

        [Transactional(TransactionalTypes.TransactionScope), UsedImplicitly]
        private void DataPortal_Delete(SingleCriteria<User, int> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentCommitRepository>())
            {
                var data = repository.First(x => x.Id == criteria.Value);
                repository.Delete(data);
            }
        }

        [UsedImplicitly]
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<InstrumentCommit, Guid> criteria)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentCommitRepository>())
            {
                repository.Delete(ExportData());
            }
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.InstrumentCommit data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(NotesProperty, data.Notes);
            LoadProperty(InstrumentIdProperty, data.InstrumentId);
            LoadProperty(RevisionProperty, data.Revision);
            LoadProperty(EffectiveToProperty, data.EffectiveTo);
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion);
        }

        private DataAccess.Database.InstrumentCommit ExportData()
        {
            SetDataStamps();

            var data = new DataAccess.Database.InstrumentCommit
            {
                Id = ReadProperty(IdProperty),
                Notes = ReadProperty(NotesProperty),
                InstrumentId = ReadProperty(InstrumentIdProperty),
                Revision = ReadProperty(RevisionProperty),
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
