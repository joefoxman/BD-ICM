using System;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartSearchResult : ReadOnlyBase<PartSearchResult>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> PartIdProperty = RegisterProperty<int>(c => c.PartId);
        public static readonly PropertyInfo<int?> ParentPartIdProperty = RegisterProperty<int?>(c => c.ParentPartId);
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public static readonly PropertyInfo<string> SerialNumberProperty = RegisterProperty<string>(c => c.SerialNumber);
        public static readonly PropertyInfo<string> DocumentNumberProperty = RegisterProperty<string>(c => c.DocumentNumber);
        public static readonly PropertyInfo<string> SapPartNumberProperty = RegisterProperty<string>(c => c.SapPartNumber);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<int> LevelProperty = RegisterProperty<int>(c => c.Level);

        public int Level
        {
            get { return GetProperty(LevelProperty); }
        }

        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
        }

        public int? ParentPartId
        {
            get { return GetProperty(ParentPartIdProperty); }
        }

        public string SapPartNumber
        {
            get { return GetProperty(SapPartNumberProperty); }
        }

        public string DocumentNumber
        {
            get { return GetProperty(DocumentNumberProperty); }
        }

        public string SerialNumber
        {
            get { return GetProperty(SerialNumberProperty); }
        }

        public string Name
        {
            get { return GetProperty(NameProperty); }
        }

        public int PartId
        {
            get { return GetProperty(PartIdProperty); }
        }

        #endregion

        #region [Factory Methods]

        public static PartSearchResult GetPartSearchResult(DataAccess.Dto.PartSearchResult data)
        {
            return DataPortal.FetchChild<PartSearchResult>(data);
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Dto.PartSearchResult data)
        {
            ImportData(data);
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Dto.PartSearchResult data)
        {
            LoadProperty(NameProperty, data.Name);
            LoadProperty(PartIdProperty, data.PartId);
            LoadProperty(ParentPartIdProperty, data.ParentPartId);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(SerialNumberProperty, data.SerialNumber);
            LoadProperty(DocumentNumberProperty, data.DocumentNumber);
            LoadProperty(SapPartNumberProperty, data.SapPartNumber);
            LoadProperty(LevelProperty, data.Level);
        }

        #endregion

    }
}
