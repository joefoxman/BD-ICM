using System;
using Csla;

namespace Bd.Icm
{
    [Serializable]
    public abstract class DataObject<T> : BusinessBase<T>, IDataObject
        where T : DataObject<T>
    {
        public static readonly PropertyInfo<DateTime> CreatedDateProperty = RegisterProperty<DateTime>(c => c.CreatedDate);
        public static readonly PropertyInfo<int> CreatedByProperty = RegisterProperty<int>(c => c.CreatedBy);
        public static readonly PropertyInfo<DateTime> ModifiedDateProperty = RegisterProperty<DateTime>(c => c.ModifiedDate);
        public static readonly PropertyInfo<int> ModifiedByProperty = RegisterProperty<int>(c => c.ModifiedBy);
        protected byte[] _rowVersion;

        public byte[] RowVersion
        {
            get { return _rowVersion; }
            internal set { _rowVersion = value; }
        }

        protected void GetConcurrencyData(byte[] version)
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

        protected void GetDataStamps(DateTime createdOn, int createdBy, DateTime updatedOn, int updatedBy, byte[] dbVersion)
        {
            LoadProperty(CreatedDateProperty, createdOn);
            LoadProperty(CreatedByProperty, createdBy);
            LoadProperty(ModifiedDateProperty, updatedOn);
            LoadProperty(ModifiedByProperty, updatedBy);
            GetConcurrencyData(dbVersion);
        }

        protected void GetDataStamps(IDataObject dataObject)
        {
            GetDataStamps(dataObject.CreatedDate, dataObject.CreatedBy, dataObject.ModifiedDate, dataObject.ModifiedBy, dataObject.RowVersion);
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
    }
}
