using Csla;
using System;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class InstrumentCommitInfo : ReadOnlyBase<InstrumentCommitInfo>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(c => c.Notes);
        public static readonly PropertyInfo<int> InstrumentIdProperty = RegisterProperty<int>(c => c.InstrumentId);
        public static readonly PropertyInfo<int> RevisionProperty = RegisterProperty<int>(c => c.Revision);
        public static readonly PropertyInfo<DateTime> CreatedDateProperty = RegisterProperty<DateTime>(c => c.CreatedDate);
        public static readonly PropertyInfo<int> EffectiveToProperty = RegisterProperty<int>(c => c.EffectiveTo);
        public static readonly PropertyInfo<string> CreatorProperty = RegisterProperty<string>(c => c.Creator);

        public string Creator => GetProperty(CreatorProperty);
        public int EffectiveTo => GetProperty(EffectiveToProperty);
        public DateTime CreatedDate => GetProperty(CreatedDateProperty);
        public int Revision => GetProperty(RevisionProperty);
        public int InstrumentId => GetProperty(InstrumentIdProperty);
        public int Id => GetProperty(IdProperty);
        public string Notes => GetProperty(NotesProperty);

        #endregion

        #region [Factory Methods]

        public static InstrumentCommitInfo GetInstrumentCommitInfo(DataAccess.Database.InstrumentCommit data)
        {
            return DataPortal.FetchChild<InstrumentCommitInfo>(data);
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.InstrumentCommit data)
        {
            ImportData(data);
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
            LoadProperty(CreatedDateProperty, data.CreatedDate);
            LoadProperty(CreatorProperty, data.Creator.UserName);
        }
        #endregion

    }
}
