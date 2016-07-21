using System;
using Bd.Icm.Core;
using Bd.Icm.DataAccess.Interfaces;
using Csla;

namespace Bd.Icm
{
    [Serializable]
    public abstract class VersionedObject<T> : BusinessBase<T>
        where T : VersionedObject<T>
    {
        public static readonly PropertyInfo<int> EffectiveFromProperty = RegisterProperty<int>(c => c.EffectiveFrom);
        public static readonly PropertyInfo<int> EffectiveToProperty = RegisterProperty<int>(c => c.EffectiveTo);
        public static readonly PropertyInfo<DateTime> CreatedDateProperty = RegisterProperty<DateTime>(c => c.CreatedDate);
        public static readonly PropertyInfo<int> CreatedByProperty = RegisterProperty<int>(c => c.CreatedBy);
        public static readonly PropertyInfo<DateTime> ModifiedDateProperty = RegisterProperty<DateTime>(c => c.ModifiedDate);
        public static readonly PropertyInfo<int> ModifiedByProperty = RegisterProperty<int>(c => c.ModifiedBy);
        public static readonly PropertyInfo<ModificationType> ModificationTypeProperty = RegisterProperty<ModificationType>(c => c.ModificationType);

        public ModificationType ModificationType
        {
            get { return GetProperty(ModificationTypeProperty); }
            internal set { SetProperty(ModificationTypeProperty, value); }
        }

        protected int _rowVersion;

        public int RowVersion
        {
            get { return _rowVersion; }
            internal set { _rowVersion = value; }
        }

        protected void GetConcurrencyData(int version)
        {
            _rowVersion = version;
        }

        public int ModifiedBy
        {
            get { return GetProperty(ModifiedByProperty); }
            set { SetProperty(ModifiedByProperty, value); }
        }

        public DateTime ModifiedDate
        {
            get { return GetProperty(ModifiedDateProperty); }
            set { SetProperty(ModifiedDateProperty, value); }
        }

        public int CreatedBy
        {
            get { return GetProperty(CreatedByProperty); }
            set { SetProperty(CreatedByProperty, value); }
        }
        public DateTime CreatedDate
        {
            get { return GetProperty(CreatedDateProperty); }
            set { SetProperty(CreatedDateProperty, value); }
        }

        protected void GetDataStamps(DateTime createdOn, int createdBy, DateTime updatedOn, int updatedBy, int rowVersion, int modificationType)
        {
            LoadProperty(CreatedDateProperty, createdOn);
            LoadProperty(CreatedByProperty, createdBy);
            LoadProperty(ModifiedDateProperty, updatedOn);
            LoadProperty(ModifiedByProperty, updatedBy);
            LoadProperty(ModificationTypeProperty, modificationType);
            GetConcurrencyData(rowVersion);
        }

        protected void GetDataStamps(VersionedObject<T> dataObject)
        {
            GetDataStamps(dataObject.CreatedDate, dataObject.CreatedBy, dataObject.ModifiedDate, dataObject.ModifiedBy, dataObject.RowVersion, (int)dataObject.ModificationType);
        }

        protected void LoadAuditFields(IAuditedRecord record)
        {
            LoadProperty(CreatedDateProperty, record.CreatedDate);
            LoadProperty(CreatedByProperty, record.CreatedBy);
            LoadProperty(ModifiedDateProperty, record.ModifiedDate);
            LoadProperty(ModifiedByProperty, record.ModifiedBy);
        }


        protected void SetDataStamps()
        {
            if (IsNew)
            {
                CreatedBy = User.Current.Id;
                CreatedDate = DateTime.Now;
            }
            ModifiedBy = User.Current.Id;
            ModifiedDate = DateTime.Now;
        }

        public int EffectiveTo
        {
            get { return GetProperty(EffectiveToProperty); }
            protected set { SetProperty(EffectiveToProperty, value); }
        }

        public int EffectiveFrom
        {
            get { return GetProperty(EffectiveFromProperty); }
            protected set { SetProperty(EffectiveFromProperty, value); }
        }

        protected void LoadVersions(IVersionedRecord data)
        {
            LoadProperty(EffectiveFromProperty, data.EffectiveFrom);
            LoadProperty(EffectiveToProperty, data.EffectiveTo);
        }

        protected bool IsInVersion(IVersionedRecord record, int version)
        {
            return ((record.EffectiveTo == int.MaxValue) && (version == int.MaxValue)) ||
                   ((version >= record.EffectiveFrom) && (version < record.EffectiveTo));
        }
    }
}
