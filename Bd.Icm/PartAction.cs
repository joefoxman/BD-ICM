using Csla;
using System;
using Bd.Icm.Core;
using Bd.Icm.DataAccess;
using Bd.Icm.Rules;
using Csla.Rules.CommonRules;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class PartAction : VersionedObject<PartAction> 
    {
        #region [Properties]

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public static readonly PropertyInfo<PartActionType> ActionProperty = RegisterProperty<PartActionType>(c => c.Action);
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public static readonly PropertyInfo<DateTime> ActionDateProperty = RegisterProperty<DateTime>(c => c.ActionDate);
        public static readonly PropertyInfo<UserInfo> ModifierProperty = RegisterProperty<UserInfo>(c => c.Modifier);

        public UserInfo Modifier => GetProperty(ModifierProperty);

        public DateTime ActionDate
        {
            get { return GetProperty(ActionDateProperty); }
            set { SetProperty(ActionDateProperty, value); }
        }

        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public PartActionType Action
        {
            get { return GetProperty(ActionProperty); }
            set { SetProperty(ActionProperty, value); }
        }

        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        #endregion

        #region [Business Rules]

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new EnumRequired<PartActionType>(ActionProperty, PartActionType.None));
            BusinessRules.AddRule(new MaxLength(DescriptionProperty, 300));
            BusinessRules.AddRule(new MaxValue<DateTime>(ActionDateProperty, DateTime.Now.Date, () => "Action date cannot be in the future."));
        }

        #endregion

        #region [Factory Methods]

        public static PartAction NewPartAction()
        {
            return DataPortal.CreateChild<PartAction>();
        }

        public static PartAction GetPartAction(DataAccess.Database.PartAction data)
        {
            return DataPortal.FetchChild<PartAction>(data);
        }

        public static void DeletePartAction(int id)
        {
            DataPortal.Delete<PartAction>(new SingleCriteria<PartAction, int>(id));
        }

        #endregion

        #region Data Access

        [UsedImplicitly]
        private void Child_Fetch(DataAccess.Database.PartAction data)
        {
            ImportData(data);
            BusinessRules.CheckRules();
        }

        [UsedImplicitly]
        private void Child_Insert(Part parent)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartActionRepository>())
            {
                SetDataStamps();
                var data = ExportData(parent);
                repository.Insert(data);
                LoadProperty(IdProperty, data.Id);
                GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
            }

            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_Update(Part parent)
        {
            if (IsSelfDirty)
            {
                using (var repository = RepositoryFactory.Instance.GetRepository<IPartActionRepository>())
                {
                    SetDataStamps();
                    var data = ExportData(parent);
                    repository.Update(data);
                    GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
                }
            }
            FieldManager.UpdateChildren(this);
        }

        [UsedImplicitly]
        private void Child_DeleteSelf(Part parent)
        {
            FieldManager.UpdateChildren(this);
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartActionRepository>())
            {
                repository.Delete(ExportData(parent));
            }
        }

        #endregion //Data Access

        #region [Data Mapping]

        private void ImportData(DataAccess.Database.PartAction data)
        {
            LoadProperty(IdProperty, data.Id);
            LoadProperty(ActionProperty, data.Action);
            LoadProperty(ActionDateProperty, data.ActionDate);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(ModifierProperty, UserInfo.GetUserInfo(data.Modifier));
            GetDataStamps(data.CreatedDate, data.CreatedBy, data.ModifiedDate, data.ModifiedBy, data.RowVersion, data.ModificationType);
        }

        internal DataAccess.Database.PartAction ExportData()
        {
            SetDataStamps();

            var data = new DataAccess.Database.PartAction
            {
                Id = ReadProperty(IdProperty),
                Action = (int)ReadProperty(ActionProperty),
                ActionDate = ReadProperty(ActionDateProperty),
                Description = ReadProperty(DescriptionProperty),
                CreatedBy = ReadProperty(CreatedByProperty),
                CreatedDate = ReadProperty(CreatedDateProperty),
                ModifiedBy = ReadProperty(ModifiedByProperty),
                ModifiedDate = ReadProperty(ModifiedDateProperty),
                RowVersion = RowVersion
            };
            return data;
        }

        internal DataAccess.Database.PartAction ExportData(Part parent)
        {
            var data = ExportData();
            data.PartId = parent.Id;
            return data;
        }
        #endregion

    }
}
