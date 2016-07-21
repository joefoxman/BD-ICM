using System.Collections.Generic;
using System.Linq;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class PartActionRepository : VersionedRepository<PartAction>, IPartActionRepository
    {
        protected override int GetNewId(PartAction entity)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartActionVersionRepository>())
            {
                return repository.GetNewId(entity.ModifiedBy);
            }
        }

        public IEnumerable<PartAction> FetchAll(int partId, int version)
        {
            return FetchAll(version).Where(x => x.PartId == partId);
        }
    }
}
