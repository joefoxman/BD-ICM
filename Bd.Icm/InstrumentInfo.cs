using System;
using Bd.Icm.Core;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class InstrumentInfo : ReadOnlyBase<InstrumentInfo>
    {
        #region [Properties]

        public static readonly PropertyInfo<int> InstrumentIdProperty = RegisterProperty<int>(c => c.InstrumentId);
        public static readonly PropertyInfo<InstrumentType> SapPartTypeProperty = RegisterProperty<InstrumentType>(c => c.SapPartType);
        public static readonly PropertyInfo<string> SerialNumberProperty = RegisterProperty<string>(c => c.SerialNumber);
        public static readonly PropertyInfo<string> NickNameProperty = RegisterProperty<string>(c => c.NickName);
        public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);

        public string Type => GetProperty(TypeProperty);

        public string SerialNumber => GetProperty(SerialNumberProperty);

        public InstrumentType SapPartType => GetProperty(SapPartTypeProperty);

        public int InstrumentId => GetProperty(InstrumentIdProperty);

        public string NickName => GetProperty(NickNameProperty);

        #endregion

        #region [Factory Methods]

        public static InstrumentInfo GetInstrumentInfo(DataAccess.Database.Instrument data)
        {
            return DataPortal.FetchChild<InstrumentInfo>(data);
        }

        #endregion

        #region [Data Access]

        [UsedImplicitly]
        private void Child_Create(int institutionId)
        {
            LoadProperty(InstrumentIdProperty, institutionId);
        }

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.Instrument data)
        {
            ImportDto(data);
        }

        #endregion

        #region [Dto Mapping]

        private void ImportDto(DataAccess.Database.Instrument dto)
        {
            LoadProperty(TypeProperty, dto.Type);
            LoadProperty(InstrumentIdProperty, dto.Id);
            LoadProperty(SapPartTypeProperty, dto.SapPartType);
            LoadProperty(NickNameProperty, dto.NickName);
            LoadProperty(SerialNumberProperty, dto.SerialNumber);
        }
        #endregion
    }
}
