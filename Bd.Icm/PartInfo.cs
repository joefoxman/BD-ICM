using System;
using Bd.Icm.Core;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartInfo : ReadOnlyBase<PartInfo>,
        IPart
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<int?> ParentPartIdProperty = RegisterProperty<int?>(c => c.ParentPartId);
        public static readonly PropertyInfo<PartType> SapPartTypeProperty = RegisterProperty<PartType>(c => c.SapPartType);
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public static readonly PropertyInfo<string> SerialNumberProperty = RegisterProperty<string>(c => c.SerialNumber);
        public static readonly PropertyInfo<string> DocumentNumberProperty = RegisterProperty<string>(c => c.DocumentNumber);
        public static readonly PropertyInfo<int?> DashNumberProperty = RegisterProperty<int?>(c => c.DashNumber);
        public static readonly PropertyInfo<int> RevisionNumberProperty = RegisterProperty<int>(c => c.RevisionNumber);
        public static readonly PropertyInfo<string> SapPartNumberProperty = RegisterProperty<string>(c => c.SapPartNumber);
        public static readonly PropertyInfo<string> LotCodeProperty = RegisterProperty<string>(c => c.LotCode);
        public static readonly PropertyInfo<string> DateCodeProperty = RegisterProperty<string>(c => c.DateCode);
        public static readonly PropertyInfo<int?> InstrumentIdProperty = RegisterProperty<int?>(c => c.InstrumentId);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<string> MfgPartNumberProperty = RegisterProperty<string>(c => c.MfgPartNumber);

        public string MfgPartNumber => GetProperty(MfgPartNumberProperty);

        private int _version;

        public string Description => GetProperty(DescriptionProperty);

        public int? InstrumentId => GetProperty(InstrumentIdProperty);

        public int? ParentPartId => GetProperty(ParentPartIdProperty);

        public string DateCode => GetProperty(DateCodeProperty);

        public string LotCode => GetProperty(LotCodeProperty);

        public string SapPartNumber => GetProperty(SapPartNumberProperty);

        public int RevisionNumber => GetProperty(RevisionNumberProperty);

        public int? DashNumber => GetProperty(DashNumberProperty);

        public string DocumentNumber => GetProperty(DocumentNumberProperty);

        public string SerialNumber => GetProperty(SerialNumberProperty);

        public PartType SapPartType => GetProperty(SapPartTypeProperty);

        public string Name => GetProperty(NameProperty);

        public int Id => GetProperty(IdProperty);

        #endregion

        #region [Factory Methods]

        public static PartInfo GetPartInfo(DataAccess.Database.Part data)
        {
            return DataPortal.FetchChild<PartInfo>(data);
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.Part data)
        {
            ImportData(data);
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.Part data)
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
            LoadProperty(MfgPartNumberProperty, data.MfgPartNumber);
            LoadProperty(LotCodeProperty, data.LotCode);
            LoadProperty(DateCodeProperty, data.DateCode);
        }

        #endregion

    }
}
