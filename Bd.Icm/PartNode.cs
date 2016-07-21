using System;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartNode : ReadOnlyBase<PartNode>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> PartIdProperty = RegisterProperty<int>(c => c.PartId);
        public static readonly PropertyInfo<int?> ParentPartIdProperty = RegisterProperty<int?>(c => c.ParentPartId);
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public static readonly PropertyInfo<int> LevelProperty = RegisterProperty<int>(c => c.Level);
        public static readonly PropertyInfo<int?> InstrumentIdProperty = RegisterProperty<int?>(c => c.InstrumentId);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);

        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
        }

        public int? InstrumentId
        {
            get { return GetProperty(InstrumentIdProperty); }
        }

        public int? ParentPartId
        {
            get { return GetProperty(ParentPartIdProperty); }
        }

        public int Level
        {
            get { return GetProperty(LevelProperty); }
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

        public static PartNode NewPartNode()
        {
            return DataPortal.Create<PartNode>();
        }

        public static PartNode GetPartNode(DataAccess.Dto.PartNode data)
        {
            return DataPortal.FetchChild<PartNode>(data);
        }

        public static void DeletePart(int id)
        {
            DataPortal.Delete<Part>(id);
        }

        #endregion

        #region Data Access

        #region Data Access - Create
        [RunLocal]
        private void Child_Create()
        {
        }

        #endregion //Data Access - Create

        #region Data Access - Fetch

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Dto.PartNode data)
        {
            ImportData(data);
        }

        #endregion //Data Access - Fetch

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Dto.PartNode data)
        {
            LoadProperty(PartIdProperty, data.PartId);
            LoadProperty(ParentPartIdProperty, data.ParentPartId);
            LoadProperty(InstrumentIdProperty, data.InstrumentId);
            LoadProperty(NameProperty, data.Name);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(LevelProperty, data.Level);
        }
        #endregion

    }
}
