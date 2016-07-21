using System;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class PartVersionRepository : Repository<PartVersion>, IPartVersionRepository
    {
        public int GetNewId(int createdBy)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartVersionRepository>())
            {
                var idEntity = new PartVersion
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = createdBy
                };
                repository.Insert(idEntity);
                return idEntity.PartId;
            }
        }
    }
}
