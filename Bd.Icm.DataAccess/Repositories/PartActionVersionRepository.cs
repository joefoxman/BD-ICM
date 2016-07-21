using System;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class PartActionVersionRepository : Repository<PartActionVersion>, IPartActionVersionRepository
    {
        public int GetNewId(int createdBy)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartActionVersionRepository>())
            {
                var idEntity = new PartActionVersion
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = createdBy
                };
                repository.Insert(idEntity);
                return idEntity.PartActionId;
            }
        }
    }
}
