using System;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class PartMetadataVersionRepository : Repository<PartMetadataVersion>, IPartMetadataVersionRepository
    {
        public int GetNewId(int createdBy)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataVersionRepository>())
            {
                var idEntity = new PartMetadataVersion
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = createdBy
                };
                repository.Insert(idEntity);
                return idEntity.PartMetadataId;
            }
        }
    }
}
